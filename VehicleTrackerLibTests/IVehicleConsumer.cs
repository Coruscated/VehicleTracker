using VehicleTrackerLib;

namespace VehicleTrackerLibTests
{
    public interface IVehicleConsumer
    {
        void Subscribe(VehicleTracker theTracker);
        void PositionHasChanged(object theTracker, double latitude, double longitude);
    }
}