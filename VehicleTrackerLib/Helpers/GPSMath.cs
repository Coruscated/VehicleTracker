using System;

namespace VehicleTrackerLib
{
    public static class GPSMath
    {
        public static double degreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public static double radiansToDegrees(double radians)
        {
            return radians / Math.PI * 180.0;
        }

        public static double milesToMeters(double miles)
        {
            return miles / 0.00062137;
        }
    }
}