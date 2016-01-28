VEHICLE TRACKER LIB

============================================================

A simple GPS Object/Vehicle Tracker that will fire an event containing the current coordinates of the object as it moves.

============================================================

SETUP:

1. Include VehicleTrackerLib.dll in your project/console app/etc. There is a release build of the DLL located in the base directory of this solution.
2. Add System.Device as a reference (needed to create GeoCoordinates, as part of the constructor).
3. Create at least 2 GeoCoordinates in an array, and then add them as a parameter to a VehicleTracker constructor.
4. Create an object that subscribes to the VehicleTracker, & add an event handler with a callback to a method of your choice.
5. Start the event loop by calling StartTracker on your tracker object.
6. The callback will fire at intervals of 3/6/9 seconds (depending on the vehicle's current speed), will contain the current GPS coords, & will automatically stop once the last waypoint GeoCoordinate included in the constructor is reached.


============================================================

CONTRIBUTE/TEST/SAMPLE APP:

1. The .sln file should be opened in VS 2015.
2. From there you can take a look at/run the SampleVehicleTracker console app, or build all & run/add to the Unit tests available in the VehicleTrackerLibTests project.


Enjoy! 
	- NTSK 2015