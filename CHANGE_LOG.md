# Throttle Controlled Avionics :: Change Log

* 2018-1016: 3.5.3 (allista) for KSP 1.4.5
	+ Added TCA grouping to facilitate docking/undocking with retaining of configurations
		- Added the ability to manually activate any TCA instance of the vessel to have, e.g. different TCA Modules enabled.
	+ Various bugfixes.
* 2018-0913: 3.5.2.3 (allista) for KSP 1.4.5
	+ Using latest AT_Utils
* 2018-0831: 3.5.2.2 (allista) for KSP 1.4.5
	+ Compiled against KSP-1.4.5
* 2018-0615: 3.5.2.1 (allista) for KSP 1.4.3
	+ Better handling of gimbal and control surface torque
	+ Several small bugfixes
* 2018-0510: 3.5.2 (allista) for KSP 1.4.3
	+ More efficient ToOrbit/Rendezvous from the ground with _GravityTurn_-like algorithm. With a lot of tweakable options.
		- When launching in-plane with the target TCA now tries to find a launch window to get closer to the target.
	+ TCA takes into account torque from control surfaces.
	+ Attitude Control uses Oscillation Detector to quench persistent low-frequency oscillations.
	+ ToOrbit/Rendezvous AP can start without active engines.
	+ Numerous fixes in Rendezvous main logic.
	+ Fixed Maneuver Autopilot logic.
	+ Fixed VTOLControl yaw problem
	+ Fixed some Attitude Control bugs.
	+ Fixed Time Warp control.
	+ Improved performance of many orbital calculations and simulations.
* 2018-0327: 3.5.1 (allista) for KSP 1.4.1
	+ Fixed collapsed window rendering.
	+ Restoring RCS limiters when disabling TCA.
* 2017-1110: 3.5.0 (allista) for KSP 1.3.1
	+ Major overhaul of orbital autopilots:
		- Deorbit autopilot performs simulation of atmospheric flight and main deceleration to determine approximate landing trajectory. Achieves much more precise landings using chutes/brakes alone; and saves fuel in powered landing.
	+ Atmospheric trajectory is drawn in magenta in Map View
	+ Deceleration trajectory is drawn in green in Map View
		- Rendezvous autopilot employs 2D gradient-descent minimization to search for optimal rendezvous trajectory in several modes:
			- Min.DeltaV - always prioritize the lowest total dV of the rendezvous (including deceleration)
			- Fast Transfer - prefers smaller total transfer times (including time before maneuver), if it does not cost too much dV
			- Manual - you can choose between calculated transfers yourself
	+ Full reimplementation of attitude control:
	+ Attitude control uses three separate PID cascades with gain scheduling to independently control the three rotational axes. This allows for much better control over a wider range of different engines, faster and more accurate action, and eliminates some long-standing auto-oscillation bugs.
		- Remote control of TCA-equipped vessels:
	+ Now you can control nearby vessels equipped with TCA without actually switching to them. The control is not full, i.e. keyboard and mouse still belong to the active vessel; but TCA interface in this mode sends commands directly to the selected vessel. This, of course, only works on loaded vessels in comm-range, and only if you have Squadron Control Module installed.
		- Default vessel configs:
	+ Now in VAB and SPH, *separately*, you can save TCA configuration of a vessel to be the default for newly created vessels. This includes state of TCA Modules and functions, so !do not forget to activate newly bought modules! or you'll miss it in flight. *I'm working on automation of that process.*
		- Numerous fixes and improvements in:
	+ Navigation/Land
	+ Navigation/Jump To
	+ Orbital/To Orbit
	+ Orbital/Match Velocity,Brake Near
	+ Smart Engines
		- Interface changes:
			- Removed "Set" buttons of numerical entry fields. To set entered value push Enter or Keypad/Enter.
	+ Rendezvous, Deorbit, Jump To, Orbital/Land autopilots use the Setup-then-Execute approach, so pressing, sya, "Land" button does not start landing immediately. All setup controls are now located inside appropriate tabs and are opened when you press the autopilot's main button.
	+ Much, much more in commit messages...*
* 2017-0621: 3.4.2 (allista) for KSP 1.3.0
	+ Improved navigation autopilot (Go To, Follow Path, etc.), especially for low TWR, slow-engine VTOLs and H/VTOL-planes.
	+ Improved usage of Manual engines for translation/horizonal propulsion.
	+ Cruise Control, when engaged, now uses forward direction instead of _low_ current velocity. That is, ships slowly drifting backward will not try to rotate 180deg on the spot.
	+ Added a button to collapse main window.
	+ Fixed ATC+PersistentRotation.
	+ Various bugfixes and small improvements.
* 2017-0605: 3.4.1.1 (allista) for KSP 1.3.0
	+ Compatible with KSP-1.3
* 2017-0222: 3.4.1 (allista) for KSP 1.2.2
	+ DELETE the old version before installing a new one
			- BUT do not delete the ThrottleControlledAvionics.user and config.xml files to preserve your settings_
	+ Changes:
					- Rendezvous Autopilot:
						- Lambert Solver now works properly for retrograde transfers.
						- In rare cases when a rendezvous maneuver went perfect and the ships are on a collision course at rendezvous point, the brake burn now accounts for ships' sizes to stop before the collision.
						- Improved trajectory optimization by using the closest elliptic transfer when a para/hyperbolic transfer time is given and by adding additional maneuver start time prescan.
						- Improved fine-tuning by selecting initial transfer time to be equal to the current nearest approach time.
						- Additional fine-tuning of the approach after Match Orbits is used only when the target is out of loading range (i.e. not yet visible).
						- Coast stage is only used when distance to target is grater then the tolerance threshold to avoid unnesessary fine-tuning.
					- Deorbit Autopilot:
						- Landing Trajectory corrects brake time to get the proper fly-over altitude. This fixes the inability to land on a very-low-gravity worlds like Minmus.
						- Improved trajectory calculation for extremly high-lat targes (i.e. poles) + improved trajectory optimization times.
						- Initial eccentricity estimation uses current vessel orbit instead of a curcular derivative. Works better, less calculations.
						- Removed redundant label from Deorbit Settings.
					- Macros:
						- Added tooltips to Macro Actions and Conditions.
						- Added Dynamic Pressure condition and Execute Maneuver action.
					- Interface:
						- Updated the Manual
						- Added separate icons for Toolbar and AppLauncher. Made the new MAN icon.
						- Made Module Selector available in flight to show the modules installed on a ship.
						- Added hotkey removal by pressing BACKSPACE when selecting the ky.
						- In Editor Window renamed TWR to TMR (thrust to Mass ratio).
			- Engine clusters are not switched while in time warp anymore.
			- TCA's time warp control stays enabled when KSP limits warp rate by altitude.
			- Added emergency dewarping if TCA's time warp is disabled and the ship is in time warp near the upcoming maneuver.
			- Maneuver offsets now take into account minimum turn time of a ship.
* 2017-0205: 3.4.0 (allista) for KSP 1.2.2
	+ User Interface Overhaul
	+ Controls were split up into Tabs and Floating Windows
	+ As a result, the main TCA window became much smaller
	+ TCA Modules Selection
	+ Now you can select which TCA modules will be installed on you ship during construction (in VAB/SPH)
	+ This may considerably decrease the amounts of controls you see in TCA window
	+ But OTA updates were disabled for safty reasons. In-flight upgrades of ship's mainframe are now considered too risky!
	+ Added the new [T+ rV-] attitude cue that targets ship's engines so that continious thrust will propell the ship towards the target while correcting any lateral drift. Very usefull for approaches from a distance.
	+ Added Smart Engines switch to Engine Profile to set S.E. mode when profile is activated.
	+ Corrected logic of the Approach stage of Rendezvous autopilot to make it safe to approach huge targes (like asteroids).
	+ Orbital autopilots are now not available when there's only Maneuver engines onboard.
	+ Many bugfixes.
* 2017-0115: 3.3.4 (allista) for KSP 1.2.2
	+ DELETE the old version before installing a new one
		- BUT do not delete the ThrottleControlledAvionics.user and config.xml files to preserve your settings
	+ Changes:
			- Added Engine Profile Autoconfiguration in Editor.
			- Added Smart Engines mode for orbital flight:
				- In this mode engines are automatically grouped by thrust direction (e.g. all aft engines and all forward engines).
				- Then, when a big change in thrust direction is needed for a maneuver (say, 90 or 180 deg), an appropriate group is selected and enabled, while other groups are disabled.
				- There are 3 methods of selecting the appropriate group:
					- 1. Min. rotation: the selected group is the closest to the needed direction.
					- 2. Min. maneuver time: if groups differ in total thrust, the selected group is the group that has the smallest maneuver time, including time needed to change ship's attitude.
					- 3. Fuel efficiency: for small delta-V this falls back to Min. maneuver time; for large delta-V the total Isp of the group is also considered, and the group with the largest Isp has much more chance to be selected.
				- Improved TimeWarp interaction with user/mods:
					- Now TCA detects if something trying to decrease time warp and surrenders its control. This allows KAC, for instance, to make its gradual dewarping. And it allows a user to press DECREASE_TIMEWARP (default ',') or STOP_TIMEWARP (default '/') buttons and take control over the warp.
					- An on-screen message will inform you of any such event.
			- Added Launch Clamps handling by Rendezvous and ToOrbit autopilots. If you forgot to configure proper stage order for the clamps, TCA will automatically release them when needed.
			- If a ship has only one active engine and this engine is coaxial with the CoM, TCA automatically changes its Role to UnBalanced. This prevents much of a surprising and undesired ship's behaviour.
			- Fixed TCA Modules availability in Science Sandbox mode.
				- Improved Rendezvous autopilot's performance in case of escape trajectories and ultra-high altitude orbits (near SOI boundary).
				- BrakeNear autopilot now informs you about too large approach distance and waits for confirmation.
			- Fixed inappropriate active/inactive state of MatchV/BrakeNear buttons.
			- Miscelaneous bugfixes.
* 2017-0104: 3.3.3 (allista) for KSP 1.2.2
	+ DELETE the old version before installing a new one
		- BUT do not delete the ThrottleControlledAvionics.user and config.xml file to preserve your settings
	+ Changes:
			- Considerably improved Bearing Control:
				- It now uses 2-PID cascade, which is a more geneal solution. Works much better for medium to heavy crafts (Max.AA ~0.1-3 rad/s2). Very high-torque crafts (Max.AA ~10-15 rad/s2) work fine, but don't react as quickly as they *possibly* could.
			- Implemented active gimbal handling. Now engines with gimbal capability will be using it for maneuvering.
			- In Editor, added highlighting of the command part where TCA is active.
			- Fixed target and orbital attitude cues calculation for Kerbol-centric orbits.
* 2016-1230: 3.3.2 (allista) for KSP 1.2.2
	+ Fixed ToOrbit Autopilot for ~0 inclination target orbits.
	+ Fixed Deorbit Autopilot for retrograde orbits.
	+ Improved ETA and brake time calculation in Point Navigator. Should help with overshooting problem in low gravity and with slow engines.
	+ Improved LandingAutopilot performance for low-thrust ships.
	+ Maneuver Autopilot now aborts the maneuver if no fuel left.
	+ Show Countdown and Thrust Duration for uncontrollable ships too.
	+ Various bugfixes.
* 2016-1219: 3.3.1 (allista) for KSP 1.2.2
	+ DELETE the old version before installing a new one
	+ BUT do not delete the ThrottleControlledAvionics.user and config.xml file to preserve your settings
	+ Changes:
		- Compiled against KSP-1.2.2
		- Fixed the "black kraken" bug.
			- In-Editor engines' balancing:
			- Added "Wet CoM" and "Dry CoM" markers. Added switching between wet/dry mass for engines' balancing.
			- When TCA is disabled InEditor, balancing is also disabled and thrust limiters are reset to 100%.
			- Deorbit Autopilot:
			- Now aware of reentry heating and tries to avoid it (tested on Eve).
			- Can automatically use brakes and/or parachutes during deceleration to conserve fuel.
			- Has configurable "Landing Settings": Use Brakes, Use Chutes, Correct Target, Land ASAP.
		- Minor bugfixes.
* 2016-1113: 3.3.0 (allista) for KSP 1.2.1
	+ DELETE the old version before installing a new one
	+ BUT do not delete the ThrottleControlledAvionics.user and config.xml file to preserve your settings
	+ Changes:
			- In-Editor engines balancing with visualization to help building balanced crafts:
			- The thrust limiters of balanced engines are set automatically, so you can see the projected in-flight performance of an engine directly in the right-click menu.
			- The TCA Editor UI now has a "Balanced: N%" indicator at the bottom that shows the thrust limiter of the most down-throttled engine. And the "HL" button to highlight engines according to their efficacy (0% magenta > 50% yellow > 99% cyan > 100% no highliting).
			- When you pick up an engine and move it, trying to attach to a ship, TCA rebalances and indicates the results (as descrived above) in real time; so you can easily achive decent initial balancing.
			- Note, that only the initial (wet) mass is used for balancing. For now.
			- Waypoints:
			- Implemented full, one-way integration with Stock/WaypointManager waypoints: Stock waypoints may be viewed as a list in TCA window and added to the navigation path (one by one or all at once). _Note, that only the navigatable waypoints on current planet are shown in the list._
			- Implemented scenario-wide repository of navigation paths.
			- Implemented full waypoint editing + altitude change by mouse.
				- Including Name, Altitude, Latitude and Longitude, Land and Pause states.
				- Added Edit button to waypoint list. A currently edited waypoint is highlited by size and color.
			- Implemented waypoints sorting in the navigation path using ^ (up) button.
			- Cosmetics:
				- Waypoints added by mouse use sequential numbers in names: "Waypoint 1", "Waypoint 2"...
				- Coordinates are now displayed as N/S, E/W instead of counterintuitive angle between 0-360 deg.
				- Displaying vertical distance to the next waypoint in  the List.
				- A waypoint set to "Land" is highlighted with size.
		- Improved Landing from Orbit:
				- Before_ the main deceleration burn the autopilot now scans the surface around the target in search of a flat patch to land on. If it is found, the target is automatically corrected, which saves the fuel later, as the need to fly around after deceleration and physically search where to land is almost completely eliminated.
			- The time a ship will need to turn from almost-horizontal to vertical orientation during deceleration is now taken into account during landing trajectory calculation. This prevents crashes of very bulky ships.
			- Corrected calculations of the amount of the fuel needed for powered landing. Fuel shortage should not come as a surprize anymore.
		- Improved VTOL Assist:
			- Improved support for very small vessels.
			- Landing gear is not deployed if a user/autopilot does not intend to land (checked by desired altitude and engines' state).
			- The deployment time of the landing gear/wheels is now taken into account; and Assist overrides vertical velocity and holds the ship's descend (if needed) untill the gear is deployed.
			- After landing it smoothly corrects ship's attitude to place it on a tilted surface (if the ship have enough control authority).
		- Improved the way Radar + Altitude Control + Point Navigation work together:
			- Ships are allowd to fly through holes in scenery if they're small enough.
			- Altitude Control and Point Navigator now properly take into account target's altitude.
			- Point Navigator also takes into account the value of altitude ahead (as calculated by Radar).
			- Changed the meaning of Waypoint's Pause button: now the ship Stops first, then pauses the game.
			- And if both Land and Pause are toggled, the ship will first land, then pause the game.
		- Improved Maneuver Autopilot:
			- Now it correctly catches the moment when the control authority becomes too low to pursue that last bit of dV.
			- Added in-flight engines' prebalancing to correctly calculate maximum thrust on unbalanced ships.
		- Improved Bearing Control and Attitude Control performance.
		- Added CPS (Collision Prevention System) and H-Translation (Use RCS for horizontal speed corrections) toggles to "advanced" and TCA Editor UI.
		- When the game is paused TCA will not process keyboard events. No more disabling TCA while typing in a savegame name.
		- Many bugfixes.
* 2016-1031: 3.2.5 (allista) for KSP 1.2.1
	+ TCA is now available on vessels that have only reaction wheels.
		- Waypoints:
		- Integrated TCA waypoints and Stock/WaypointManager waypoints: currently active Stock waypoint can be used as a target for _Go To/Jump To_ programs and added as TCA waypoint for _Follow Path_ program.
		- Made TCA waypoints draggable: _point the mouse at a waypoint, leftclick-and-drag to change its location; release the left button to confirm; press the right button (while still pressing the left one) to cancel._
		- Improved waypoints/path overlay:
			- Waypoints now fade out with distance-along-the-path; but they also fade in with distance from the camera to the ship, so that when you zoom out from the ship the whole path becomes visible.
			- Added colored lines that connect waypoints along the path.
		- Integrated TCA with KerbNet. TCA now acts as a stand-alone computer that can fly a ship out of signal. But TCA controls will become locked, so you can't control such a ship. _Squadron Mode_ now also uses KerbNet to establish connection with controlled vessels.
		- Level_ program and _VTOL Mode_ do not take manual thrust into account anymore.
	+ Improved maneuver execution: Throttle Control now takes into account current steering request when applying differential thrust limit. Maneuver Autopilot automatically disables itself in case of inadequate thrusting near the end of the maneuver.
	+ Improved landing trajectory calculation and landing algorithm: better support for ships with low Thrust/Mass ratio; fixed rapid engine bursts during final deceleration.
	+ Added CPS switch in "advanced" pane to toggle Collision Prevention System.
			- Bugfix: a kerbal EVA does not trigger CPS response if it is on a ladder that belongs to the ship.
	+ Added H-Translation switch in "advanced" pane to disable usage of translation for horizontal speed corrections.
		- Jump To_ now also autosaves the game, like the _Land_ program does.
	+ Fixed TCA part info updating in Editor.
	+ Added Toggle Action Groups and Set Action Groups macro actions.
	+ Fixed macro saving/loading.
	+ Moved ToolbarWrapper to AT_Utils and updated it.
* 2016-1015: 3.2.4 (allista) for KSP 1.2
	+ DELETE the old version before installing a new one
		- BUT do not delete the ThrottleControlledAvionics.user and config.xml file to preserve your settings
	+ Changes:
				- Compatible with KSP-1.2
			- ToOrbit Autopilots
					- Added user-definable slope parameter.
					- Engines will now always have full thrust until initial apoapsis is reached.
					- Fixed inclination correction algorithm.
			- ThrottleControl now applies Angular Error correction differently: it will not affect engines that provide torque, so that a ship could retain maneuverability.
			- Main throttle is set to 0 on launch if a ship have active engines (in the active profile).
			- Improved support of slow engines.
			- Bugfixes.
* 2016-0904: 3.2.3 (allista) for KSP 1.1.3
	+ Bugfixes:
		- Fixed rendering of surface markers.
		- Fixed incorrect calculation of Landing Trajectory in some cases.
		- Fixed a bug in maneuver execution that caused it to block sometimes when a maneuver is finished.
		- Fixed and improved landing algorithm of Deorbit Autopilot.
		- Fixed Radar bug that was caused by not resetting the Altimeter.
	+ Improved ToOrbit autopilot algorithm.
	+ Improved low altitude landing algorithm.
	+ Ballistic Jump now uses the Minimum Energy trajectory to conserve fuel. Also fixed instability in gradient-descent optimization of the jump trajectory.
	+ Improved Deorbit Autopilot:
		- Better initial orbit calculation.
		- Added orbit correction that ensures that dorbiting ship will have at least 2km above the target after the main deceleration.
		- Improved landing algorithm.
		- Improved detection and handling of landscape obstacles.
	+ Attitude Control uses new Oscillation Detector to quench sporadic oscillations in control output.
	+ Improved PersistentRotation management.
	+ The "Enabled" button now flashes red if TCA is disabled when flying.
* 2016-0816: 3.2.2 (allista) for KSP 1.1.3
	+ Bugfixes:
		- TCA no longer activates a part where it is installed (parts that combine command module with engines should work fine now).
		- Fixed incompatibility with RealismOvehaul.
		- Fixed and improved initial trajectory calculation in DeorbitAutopilot/BallisticJump.
		- In career game: locked maneuver nodes no longer cause NREs in ToOrbit autopilot.
	+ Improved orbital landing algorithm; should work better with both stock and FAR aerodynamics.
	+ TCA is now aware of PersistentRotation and will wait for the ship to stop rotation before entering the TimeWarp.
	+ Added "Status" page to TCA Manual. It shows a warning if TCA is installed improperly. And in career mode it displays purchased TCA modules (including the main subsystem) and their statuses.
* 2016-0710: 3.2.1.1 (allista) for KSP 1.1.3
	+ Found and fixed a bug in AttitudeControl.
* 2016-0710: 3.2.1 (allista) for KSP 1.1.3
	+ Numerous fixes and improvements to:
		- Deorbit autopilot_
		- Landing autopilot_
		- Attitude_ and _Bearing_ control
	+ Compiled against _AT_Utils_ library that will now be common to all my mods. It is distributed with TCA, so no additional action is required.
	+ The naming convention of the `.glob` and `.user` files has changed: these files are now named `ThrottleControlledAvionics.glob` and `ThrottleControlledAvionics.user`. If you have done any customizations, don't forget to rename your `TCA.user` file accordingly.
* 2016-0628: 3.2.0 (allista) for KSP 1.1.3
	+ KSP-1.1.3 compatible
	+ Added orbital autopilots ([see the video](https://www.youtube.com/watch?v=l1e2brjWgBA)):
			- ToOrbit autopilot that achieves a circular orbit defined by radius and inclination, prograde or retrograde. It is smart about high orbits as it uses the double-burn technique, which is how the real satellites get to the geostationary orbit.
			- Rendezvous autopilot that can bring a ship to its target-in-orbit from anywhere in the same sphere of influence: from orbit, from a suborbital trajectory and from the surface.
			- Deorbit autopilot that lands a ship from orbit precisely at the selected target.
			- Ballistic jump that brings a ship to a target located far away on the surface of the same planet using a ballistic trajectory. It is, of course, better to use it in vacuum or sparse atmosphere.
	+ Added VTOL Control module that allows you to control a VTOL like a copter in a remote-controller style ([see the video](https://www.youtube.com/watch?v=VWFwzOA9YG0)).
		- Renamed two engine modes. I hope this way it's more intuitive:
		- Main = "Thrust & Maneuver"
		- Balanced = "Thrust"
		- Control-freak special: added a set of switches to the advanced tab that allow to disable automatic usage of Staging, Landing Gear, Brakes and Parachutes.
	+ Added verbose status messages at the bottom of the TCA window. _These are persistent and may be dismissed by clicking on them._
		- Numerous bugfixes and improvements in almost every module.
* 2016-0430: 3.1.0-KSP-1.1 (allista) for KSP 0-1.1 PRE-RELEASE
	+ This is the v3.1.0 compiled for KSP-1.1.2
* 2016-0430: 3.1.0 (allista) for KSP 1.0.5
	+ Added bearing controls to main GUI and keyboard. Works independently of CruiseControl, e.g. you may set the bearing in Stop or Level mode.
	+ Added UnBalanced engine mode which is a mix of Main and Manual: like Main it responses to altitude/vert.velocity controls, but like Manual it is not used for controlled torque generation. Like Manual engine it may be balanced with other engines in Main, Maneuver or Balanced modes. The main use: to make single-engine balanced-by-design VTOLs and rocket landers.
	+ Added configurable button colors. See the top the _TCA.glob_ file for details.
	+ Settings in _TCA.glob_ could (and should) now be overwritten in GameData/ThrottleControlledAvionics/TCA.user file. Read [the manual](https://github.com/qfeys/ThrottleControlledAvionics/blob/master/GameData/ThrottleControlledAvionics/INSTRUCTIONS.md#tcaglob--the-global-tca-settings) for details.
* 2016-0404: 3.0.1-KSP-1.1 (allista) for KSP 0-1.1 PRE-RELEASE
	+ This is the TCA-3.0.1 build for KSP-1.1 pre-release.
		- consider it a BETA release !!!
* 2016-0404: 3.0.1 (allista) for KSP 1.0.5
	+ Fixed single-engine optimization bug.
	+ Fixed onStage profile update bug.
	+ Improved HSC behaviour at very low maximum TWR of slow engines by adding correction for negative vertical speed.
	+ TCA GUI now only shows when the active vessel is loaded and unpacked.
* 2016-0401: 3.0.0 (allista) for KSP 1.0.5
	+ TCA is now a Part Module running in a ship's cockpit/probe-core. This means many ships may run TCA simultaneously.
	+ TCA has TONS of new features. Please, read [The Manual](https://github.com/qfeys/ThrottleControlledAvionics/blob/master/GameData/ThrottleControlledAvionics/INSTRUCTIONS.md).
	+ These features are divided into modules and distributed along the TechTree in career mode. Even more, there are cross-module dependencies that do not follow the stock tree. Please, consult [TCA Tech Tree](https://drive.google.com/file/d/0B3yiXDvgwkLMMWNHTUdwMkpRYUk/view) graph to know more.
	+ Acknowledgments: I want to express extreme gratitude to [smjjames](http://forum.kerbalspaceprogram.com/index.php?/profile/134004-smjjames/), who helped me so much with the testing and proposed countless improvements. Without it this version would never have come to a release!
* 2015-1110: 2.3.0.2 (allista) for KSP 1.0.5
	+ Recompiled for KSP-1.0.5
* 2015-0725: 2.3.0.1 (allista) for KSP 1.0.4
	+ Fixed issues with engines' balancing when Vertical Speed Control is disabled.
	+ Fixed handling of flameouted engines.
	+ Fixed issues with changing TCA key on some OSes.
* 2015-0722: 2.3.0 (allista) for KSP 1.0.4
	+ Added "Balanced Thrust" engine Role. With it you may use jets and other slow-response engines as a main lifting force; they are still unusable for maneuvering.
	+ Improved Vertical Speed Control System: actual vertical speed is much closer to the set-point now.
	+ Improved flip-over prevention system of Kill Horizontal Speed mode: it now considers current TWR and decreases the pitch angle if it drops too much. Also useful for jets, as they tend to loose power when turned against the air stream.
	+ Several smaller improvements.
* 2015-0624: 2.2.1.1 (allista) for KSP 1.0.4
	+ KSP 1.0.4 compatible
	+ Fixed maxThrust adjustment with velCurve; added adjustment with atmCurve.
* 2015-0504: 2.2.1 (allista) for KSP 0.90
	+ Compatible with KSP-1.0.2
	+ Fixed compatibility issues with the new KSP-API
	+ Added UseStockAppLauncher flag into TCA.glob to force the use of AppLauncher even if the Toolbar is installed.
* 2015-0327: 2.2.0 (allista) for KSP 0.90
	+ Added engine modes: Main, Maneuver, Manual.
	+ Improved balancing of engines that have velocity curves.
	+ Torque from SRBs and other throttle-locked engines is now accounted for.
	+ Tuning parameters are now hidden when Autotuning is enabled.
	+ Many performance improvements.
	+ Fixed the conflict with FRMS window.
* 2015-0212: 2.1.0 (allista) for KSP 0.90
	+ Added an autopilot to kill horizontal speed. Useful for landing.
	+ Added per-game configuration; vessel configs are not lost on game switch anymore.
	+ Fixed the regression in optimization algorithm and improved balancing of asymmetric designs.
	+ Fixed problems with steering from the External Command Seat.
	+ Other minor fixes.
* 2015-0131: 2.0.2 (allista) for KSP 0.90
	+ Integrated TCA into career mode. You will have to purchase it in Specialized Controls to use it.
	+ Improved optimization algorithm. Controls should be smoother and thrust should not decrease when torque demand is inadequate.
	+ TCA key binding is configurable from the GUI now.
	+ Added an option to control vertical speed slider with throttle controls.
* 2015-0122: 2.0.1 (allista) for KSP 0.90
	+ DELETE the old version before installing a new one
		- BUT do not delete the TCA.conf file to preserve your settings_
	+ Modified optimization algorithm to work properly with engines which minThrust is not zero; Vertical Speed Factor is now also taken into account.
	+ Corrected Toolbar/AppLauncher icon switching with TCA state.
	+ When Loosing Altitude, vertical speed is still displayed.
	+ Positions of the windows are now properly saved and loaded.
	+ Fixed KSP-AVC .version file to comply with the strict JSON and to work with CKAN.
* 2015-0119: 2.0.0 (allista) for KSP 0.90
	+ DELETE the old version before installing a new one
		- BUT do not delete the TCA.conf file to preserve your settings_
	+ Changes since v1.4.1
			- Drastically improved in-flight stability by completely changing the way engines' limits are calculated. Now it is done via iterative optimization of the error function.
			- Added smooth vertical speed control which allows not only hovering, but also fast and agile horizontal flight; even with very unbalanced designs.
			- Added several configurable parameters that allow to tune TCA even for the most bizarre ship designs.
			- Added automatic parameter tuning that works well with stock SAS and standard ship builds.
			- Added sophisticated settings system with separate Global, Per-Vessel and Named configurations. Configuration files are automatically generated on first launch and are located under `Plugins/PluginData/ThrottleControlledAvionics`
				- The default TCA.glob file contains, among other presets, the key binding for enabling TCA.
* 2015-0115: 1.5.0-beta.2 (allista) for KSP 0.90 PRE-RELEASE
	+ warning:  THIS IS BETA :warning: !!!
	+ warning: BACKUP YOU SAVES :warning: !!!
	+ Completely changed the way engines' limits are calculated. Now it is done using iterative optimization of the error function. This considerably improves in-flight stability with respect to beta.0, especially with unbalanced designs.
	+ Improved response time, precision and stability of the Vertical Speed control.
	+ Added PI controller for engines to smooth out thrust jitter.
	+ Added Master, Pitch, Yaw and Roll gains to tune control response.
	+ Added sophisticated settings system with separate Global, Per-Vessel and Named configurations. Configuration file is automatically created on first launch and is located under Plugins/PluginData/.
		- Among other things added configurable TCA key binding.
	+ Removed main engine detection: new algorithm accounts for them automatically.
	+ Disabling TCA will not set the Main Throttle to 0 anymore.
	+ Updated Help text.
* 2015-0108: 1.5.0-beta.0 (allista) for KSP 0.90 PRE-RELEASE
	+ warning:  THIS IS BETA :warning: !!!
	+ warning: BACKUP YOU SAVES :warning: !!!
	+ The plugin mechanics were rewritten almost from scratch and the result was tested in only a few situations. If you're using this version, be careful, and please, do report any bugs you encounter.
	+ Changes:
		- Drastically improved in-flight stability. SAS now performs only slight corrections which saves fuel and battery (if the ship has reaction wheels).
		- Added smooth vertical speed control which allows not only hovering, but also fast and agile horizontal flight; even with very unbalanced designs.
		- TCA does not handle main engines anymore. Thrust limit of any engine that does not produce torque is not changed. You should handle such engines with Action Groups or manually.
		- When TCA is disabled, it resets thrust limit AND disables main throttle.
		- Almost all calculations are completely automatic, so I removed many of the controls.
			- For now_ settings do not work. Each time you switch to a ship you should tweak them again. This of course will be changed in the next STABLE release.
* 2015-0108: 1.4.1 (allista) for KSP 0.90
	+ KSP-0.90 compatible
	+ DELETE the old version before installing a new one
		- BUT do not delete the TCASave.txt file to preserve your settings_
	+ Made Toolbar support optional.
	+ Added basic support of the stock AppLauncher.
	+ Added KSP-AVC support.
	+ Fixed the "NullRef on engine destroyed" bug.
	+ Optimized performance a little.
	+ Changed Mod folder structure to canonical form.
