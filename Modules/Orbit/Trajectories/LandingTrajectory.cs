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

namespace ThrottleControlledAvionics
{
	public class LandingTrajectory : TargetedTrajectory
	{
		public readonly double TargetAltitude;
		public QuaternionD AtSurfaceRotation { get; private set; }
		public QuaternionD AtStartRotation { get; private set; }
		public WayPoint SurfacePoint { get; private set; }

		public double VslStartLat { get; private set; }
		public double VslStartLon { get; private set; }

		public double DeltaLat { get; private set; }
		public double DeltaLon { get; private set; }

		//distance in radial coordinates with the center at the vessel
		/// <summary>
		/// Radial difference between the target and the landing site in degrees.
		/// </summary>
		public double DeltaR { get; private set; } = 180;

		Vector3d brake_delta_v;
		public override Vector3d BrakeDeltaV { get { return brake_delta_v; } }
		public float  BrakeDuration;
		public double BrakeStartUT { get; private set; }
		public double BrakeEndUT { get; private set; }
		public double TimeToSurface { get { return AtTargetUT-VSL.Physics.UT; } }

		public LandingTrajectory(VesselWrapper vsl, Vector3d dV, double startUT, 
		                         WayPoint target, double target_altitude = 0, bool with_brake = true)
			: base(vsl, dV, startUT, target)
		{
			TargetAltitude = target_altitude;
			update(with_brake);
		}

		void update_from_orbit(Orbit orb, double UT)
		{
			//calculate the position of a landing site
			if(orb.ApA <= TargetAltitude) AtTargetUT = orb.StartUT+(orb.trueAnomaly < 180? orb.timeToAp : 1);
			else AtTargetUT = TrajectoryCalculator.NearestRadiusUT(orb, Body.Radius+TargetAltitude, UT);
			TimeToTarget = AtTargetUT-StartUT;
			AtSurfaceRotation = TrajectoryCalculator.InvBodyRotationAtdT(Body, TimeToSurface);
			AtTargetPos = orb.getRelativePositionAtUT(AtTargetUT);
			AtTargetVel = orb.getOrbitalVelocityAtUT(AtTargetUT);
			SurfacePoint = new WayPoint(AtSurfaceRotation*(AtTargetPos.xzy)+Body.position, Body);
			SurfacePoint.Name = "Landing Site";
		}

		void update(bool with_brake)
		{
			update_from_orbit(NewOrbit, NewOrbit.trueAnomaly > 180? StartUT : StartUT+NewOrbit.timeToAp);
			AtStartRotation = TrajectoryCalculator.InvBodyRotationAtdT(Body, TimeToStart);
			//correct for brake maneuver
			if(with_brake)
			{
				BrakeEndUT = AtTargetUT-GLB.LTRJ.CorrectionOffset;
				BrakeStartUT = BrakeEndUT-MatchVelocityAutopilot.BrakingOffset((float)AtTargetVel.magnitude, VSL, out BrakeDuration);
				brake_delta_v = -0.9*NewOrbit.getOrbitalVelocityAtUT(BrakeEndUT);
				update_from_orbit(TrajectoryCalculator.NewOrbit(NewOrbit, BrakeDeltaV, BrakeEndUT), BrakeEndUT);
			}
			else
			{
				brake_delta_v = -(AtTargetVel + Vector3d.Cross(Body.angularVelocity.xzy, AtTargetPos));
				BrakeEndUT = TrajectoryCalculator.FlyAboveUT(NewOrbit, Target.RelSurfPos(Body).xzy, StartUT);
				BrakeStartUT = BrakeEndUT-MatchVelocityAutopilot.BrakingOffset((float)BrakeDeltaV.magnitude, VSL, out BrakeDuration);
			}
			//compute vessel coordinates at maneuver start
			if(VSL.LandedOrSplashed)
			{
				VslStartLat = Utils.ClampAngle(VSL.vessel.latitude);
				VslStartLon = Utils.ClampAngle(VSL.vessel.longitude);
			}
			else
			{
				var start_pos = AtStartRotation*(StartPos.xzy) + Body.position;
				VslStartLat = Utils.ClampAngle(Body.GetLatitude(start_pos));
				VslStartLon = Utils.ClampAngle(Body.GetLongitude(start_pos));
			}
			//compute distance to target
			DistanceToTarget = Target.AngleTo(SurfacePoint)*Body.Radius;
			//compute distance in lat-lon coordinates
			DeltaLat = Utils.AngleDelta(SurfacePoint.Pos.Lat, Target.Pos.Lat)*
				Math.Sign(Utils.AngleDelta(Utils.ClampAngle(VslStartLat), SurfacePoint.Pos.Lat));
			DeltaLon = Utils.AngleDelta(SurfacePoint.Pos.Lon, Target.Pos.Lon)*
				Math.Sign(Utils.AngleDelta(Utils.ClampAngle(VslStartLon), SurfacePoint.Pos.Lon));
			//compute distance in radial coordinates
			DeltaFi = 90-Vector3d.Angle(NewOrbit.GetOrbitNormal().xzy,
			                            QuaternionD.Inverse(AtSurfaceRotation) * Body.GetRelSurfacePosition(Target.Pos.Lat, Target.Pos.Lon, TargetAltitude));
			DeltaR = Utils.RadDelta(SurfacePoint.AngleTo(VslStartLat, VslStartLon), Target.AngleTo(VslStartLat, VslStartLon))*Mathf.Rad2Deg;

//			Utils.Log("{0}", this);//debug
		}

		public Vector3d GetOrbitVelocityAtSurface()
		{ return NewOrbit.getOrbitalVelocityAtUT(AtTargetUT); }

		public override void UpdateOrbit(Orbit current)
		{
			base.UpdateOrbit(current);
			update(false);
		}

		public void UpdateOrbit(Orbit current, bool with_brake)
		{
			base.UpdateOrbit(current);
			update(with_brake);
		}

		public override string ToString()
		{
			return base.ToString()+
				Utils.Format("\nLanding Site: {},\n" +
				             "TimeToSurface: {}\n" +
		                     "Delta R: {} deg\n" +
				             "Delta Lat: {} deg, Delta Lon: {} deg\n" +
				             "Brake DeltaV: {}\n" +
				             "Brake Duration: {}, Time to Brake: {}",
				             SurfacePoint, TimeToSurface,
				             DeltaR, DeltaLat, DeltaLon,
				             BrakeDeltaV, BrakeDuration, BrakeStartUT-VSL.Physics.UT);
		}
	}

	public class PlaneChangeTrajectory : TargetedTrajectory
	{
		public PlaneChangeTrajectory(VesselWrapper vsl, Vector3d dV, double startUT, WayPoint target)
			: base(vsl, dV, startUT, target) { update(); }

		void update()
		{ 
			DeltaFi = TrajectoryCalculator.RelativeInclinationAtResonance(NewOrbit, Target.RelOrbPos(Body), StartUT, out TimeToTarget); 
			DistanceToTarget = DeltaFi;
			AtTargetUT = StartUT+TimeToTarget;
		}

		public override void UpdateOrbit(Orbit current)
		{
			base.UpdateOrbit(current);
			update();
		}
	}
}

