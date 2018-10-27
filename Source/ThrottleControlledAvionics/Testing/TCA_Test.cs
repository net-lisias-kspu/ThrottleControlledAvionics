﻿//   TCA_Test.cs
//
//  Author:
//       Allis Tauri <allista@gmail.com>
//
//  Copyright (c) 2017 Allis Tauri
using System;
using UnityEngine;
using AT_Utils;

namespace ThrottleControlledAvionics
{
    public abstract class TCA_Test : ITestScenario
    {
        static Globals GLB { get { return Globals.Instance; } }

        protected ModuleTCA TCA;
        protected VesselWrapper VSL { get { return TCA.VSL; } }
        protected VesselConfig CFG { get { return TCA.CFG; } }
        protected readonly string Name;
        protected readonly string LogFile;

        protected bool GetTCA()
        {
            if(FlightGlobals.ActiveVessel != null)
                TCA = ModuleTCA.EnabledTCA(FlightGlobals.ActiveVessel);
            return TCA != null;
        }

        protected TCA_Test()
        {
            Name = GetType().Name;
            LogFile = Name+DateTime.Now.ToString("-yyyy-MM-dd_HH-ss")+".log";
        }

        protected void Log(string msg, params object[] args)
        {
            msg = string.Format("{0}: {1}", GetType().Name, msg);
            Utils.Log2File(LogFile, msg, args);
            Utils.Log(msg, args);
        }

        protected void LogFlightLog(string msg, params object[] args)
        {
            msg += string.Format("\n{0}\n\n{1}\n", 
                                 string.Join("\n", FlightLogger.eventLog.ToArray()),
                                 FlightLogger.getMissionStats());
            Log(msg, args);
        }

        protected void RotateMapView()
        {
            PlanetariumCamera.fetch.camHdg = (PlanetariumCamera.fetch.camHdg-0.001f)%(float)Utils.TwoPI;
            var pitch = Utils.CenterAngle(VSL.orbit.inclination+90);
            if(pitch > 90) pitch = 180-pitch;
            else if(pitch < -90) pitch = -180-pitch;
            PlanetariumCamera.fetch.camPitch = (float)pitch*Mathf.Deg2Rad;
        }

        protected void ResetFlightCamera()
        {
            if(VSL != null)
            {
                FlightCamera.fetch.camHdg = 0;
                FlightCamera.fetch.camPitch = 0;
                FlightCamera.fetch.SetDistanceImmediate(VSL.Geometry.D*2);
                FlightCamera.SetModeImmediate(FlightCamera.Modes.AUTO);
            }
        }

        #region ITestScenario implementation
        public abstract string Setup();
        public abstract bool Update(System.Random RND);
        public abstract void Cleanup();

        public virtual void Draw() {}

        public virtual string Status { get; protected set; } = "";
        public abstract bool NeedsFixedUpdate { get; }
        public abstract bool NeedsUpdate { get; }
        #endregion
    }
}

