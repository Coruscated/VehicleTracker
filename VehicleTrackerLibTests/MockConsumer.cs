using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackerLib;

namespace VehicleTrackerLibTests
{
    public class MockConsumer : IVehicleConsumer
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public void Subscribe(VehicleTracker theTracker)
        {
            theTracker.PositionChange +=
               new VehicleTracker.PositionChangeHandler(PositionHasChanged);
        }

        public void PositionHasChanged(
           object theTracker,
           double lat,
           double lng
           )
        {
            latitude = lat;
            longitude = lng;
        }
    }
}
