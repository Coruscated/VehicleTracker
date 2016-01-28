using System;
using VehicleTrackerLib;
using System.Device.Location;

namespace SampleVehicleTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Must contain at least 2 valid coordinates
            GeoCoordinate[] waypoints = new GeoCoordinate[]
            {
                new GeoCoordinate(38.048196, -121.089409),
                new GeoCoordinate(38.041013, -121.091238),
                new GeoCoordinate(37.984013, -121.103238)
            };

            VehicleTracker gpsTracker = new VehicleTracker(waypoints);

            Vehicle car = new Vehicle();
            car.Subscribe(gpsTracker);

            gpsTracker.StartTracker();
        }
    }
}
