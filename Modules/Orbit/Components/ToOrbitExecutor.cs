﻿//  Author:
//       Allis Tauri <allista@gmail.com>
//
//  Copyright (c) 2016 Allis Tauri
//
// This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
// To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/ 
// or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
//
using System;
using UnityEngine;
using AT_Utils;

namespace ThrottleControlledAvionics
{
	public class ToOrbitExecutor : OrbitalComponent
	{
		const double MinClimbTime = 5;

		ThrottleControl THR;
		BearingControl BRC;

		readonly SingleAction GearAction = new SingleAction();
		readonly FuzzyThreshold<double> ErrorThreshold = new FuzzyThreshold<double>();
		readonly ManeuverExecutor Executor;

		Vector3d target;
		public Vector3d Target 
		{ 
			get { return target; } 
			set { target = value; TargetR = target.magnitude; } 
		}
		public double TargetR { get; private set; }
		public double LaunchUT = -1;
		public double ApAUT = -1;
		public double dApA { get; private set; }
		public double GravityTurnStart { get; private set; }

		double CircularizationOffset = -1;

		public ToOrbitExecutor(ModuleTCA tca) : base(tca) 
		{ 
			InitModuleFields();
			Executor = new ManeuverExecutor(tca);
			GearAction.action = () =>VSL.GearOn(false);
			ErrorThreshold.Lower = 2*GLB.ORB.Dtol;
			ErrorThreshold.Upper = 4*GLB.ORB.Dtol;
			GravityTurnStart = 0;
		}

		public void UpdateTargetPosition()
		{
			Target = QuaternionD.AngleAxis(Body.angularV*TimeWarp.fixedDeltaTime*Mathf.Rad2Deg, 
			                               Body.zUpAngularVelocity.normalized)*Target;
		}

		public bool Liftoff()
		{
			UpdateTargetPosition();
			VSL.Engines.ActivateInactiveEngines();
			if(VSL.VerticalSpeed.Absolute/VSL.Physics.G < MinClimbTime)
			{ 
				Status("Liftoff...");
				CFG.DisableVSC();
				CFG.VTOLAssistON = true;
				THR.Throttle = 1;
				return true;
			}
			GravityTurnStart = VSL.Altitude.Absolute;
			GearAction.Run();
			CFG.VTOLAssistON = false;
			CFG.StabilizeFlight = false;
			CFG.HF.Off();
			return false;
		}

		/// <summary>
		/// The arc distance in radians between current vessel position and the Target.
		/// </summary>
		public double ArcDistance
		{ get { return Utils.ProjectionAngle(VesselOrbit.pos, target, target-VesselOrbit.pos) * Mathf.Deg2Rad; } }

		public bool GravityTurn(double ApA_offset, double gturn_curve, double dist2vel, double Dtol)
		{
			UpdateTargetPosition();
			dApA = TargetR-VesselOrbit.ApR;
			var vel  = Vector3d.zero;
			var cApV = VesselOrbit.getRelativePositionAtUT(VSL.Physics.UT+VesselOrbit.timeToAp);
			var hv   = Vector3d.Exclude(VesselOrbit.pos, target-VesselOrbit.pos).normalized;
			var arc  = Utils.ProjectionAngle(cApV, target, hv)*Mathf.Deg2Rad*cApV.magnitude;
			ErrorThreshold.Value = dApA+arc;
			if(!ErrorThreshold)
			{
				var startF = Utils.Clamp((VSL.Altitude.Absolute-GravityTurnStart)/GLB.ORB.GTurnOffset, 0, 1);
				if(dApA > Dtol)
					vel += VSL.Physics.Up.xzy*dApA*gturn_curve;
				if(arc > Dtol)
				{
					var hvel = Utils.ClampL(arc-dApA*gturn_curve, 0)*startF;
					if(Body.atmosphere) hvel *= Math.Sqrt(Utils.Clamp(VSL.Altitude.Absolute/Body.atmosphereDepth, 0, 1));
					vel += hv*hvel;
				}
				vel *= VSL.Physics.StG/Utils.G0*dist2vel;
				if(!vel.IsZero())
				{
					var norm = VesselOrbit.GetOrbitNormal();
					var dFi = (90-Vector3d.Angle(norm, target))*Mathf.Deg2Rad;
					vel += norm*Math.Sin(dFi)*vel.magnitude*startF
						*Utils.Clamp(VSL.VerticalSpeed.Absolute/VSL.Physics.G-MinClimbTime, 0, 100)
						*Utils.ClampL(Vector3d.Dot(hv, VesselOrbit.vel.normalized), 0);
				}
				vel = vel.xzy;
				CircularizationOffset = -1;
				if(Executor.Execute(vel, Utils.Clamp(1-VSL.Torque.MaxCurrent.AA_rad, 0.1f, 1)))
				{
					if(CFG.AT.Not(Attitude.KillRotation)) 
					{
						CFG.BR.OnIfNot(BearingMode.Auto);
						BRC.ForwardDirection = hv.xzy;
					}
					Status("Gravity turn...");
					return true;
				}
			}
			Status("Coasting...");
			CFG.BR.OffIfOn(BearingMode.Auto);
			CFG.AT.OnIfNot(Attitude.KillRotation);
			THR.Throttle = 0;
			if(CircularizationOffset < 0)
			{
				ApAUT = VSL.Physics.UT+VesselOrbit.timeToAp;
				CircularizationOffset = VSL.Engines.TTB((float)TrajectoryCalculator.dV4C(VesselOrbit, hV(ApAUT), ApAUT).magnitude)/2;
			}
			return VesselOrbit.timeToAp > ApA_offset+CircularizationOffset &&
				Body.atmosphere && VesselOrbit.radius < Body.Radius+Body.atmosphereDepth;
		}
	}
}

