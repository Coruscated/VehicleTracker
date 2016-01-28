using System;
using VehicleTrackerLib;

public class Vehicle
{
    public void Subscribe(VehicleTracker theTracker)
    {
        theTracker.PositionChange +=
           new VehicleTracker.PositionChangeHandler(PositionHasChanged);
    }

    public void PositionHasChanged(
       object theTracker,
       double latitude,
       double longitude)
    {
        Console.WriteLine("Current GPS coordinates [Lat,Long]: [{0}, {1}]",
           latitude,
           longitude);
    }
}