using System;
using System.Device.Location;

namespace VehicleTrackerLib
{
    public class GPSCalc
    {
        public static GeoCoordinate moveTowards(GeoCoordinate start, GeoCoordinate end, double distance)
        {
            var currLatitude = GPSMath.degreesToRadians(start.Latitude);
            var currLongitude = GPSMath.degreesToRadians(start.Longitude);
            var endLatitude = GPSMath.degreesToRadians(end.Latitude);
            var endLongitude = GPSMath.degreesToRadians(end.Longitude);
            var longitudeDist = GPSMath.degreesToRadians(end.Longitude - start.Longitude);

            var bearing = Math.Atan2(
                Math.Sin(longitudeDist) * Math.Cos(endLatitude),
                Math.Cos(currLatitude) * Math.Sin(endLatitude) -
                Math.Sin(currLatitude) * Math.Cos(endLatitude) *
                Math.Cos(longitudeDist)
                );


            double angularDist = distance / 6371000.0;

            endLatitude = Math.Asin(
                Math.Sin(currLatitude) * Math.Cos(angularDist) +
                Math.Cos(currLatitude) * Math.Sin(angularDist) *
                Math.Cos(bearing)
                );

            endLongitude = currLongitude +
                Math.Atan2(
                    Math.Sin(bearing) * Math.Sin(angularDist) *
                    Math.Cos(currLatitude),
                    Math.Cos(angularDist) - Math.Sin(currLatitude) * Math.Sin(endLatitude)
                );

            if (double.IsNaN(endLatitude) || double.IsNaN(endLongitude))
            {
                throw new ArgumentOutOfRangeException();
            }

            return new GeoCoordinate(GPSMath.radiansToDegrees(endLatitude), GPSMath.radiansToDegrees(endLongitude));
        }
    }
}