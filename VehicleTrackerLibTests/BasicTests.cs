using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleTrackerLib;
using System.Device.Location;

namespace VehicleTrackerLibTests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_InvalidCoordinateQuantity()
        {
            VehicleTracker trk = new VehicleTracker(new GeoCoordinate[] { new GeoCoordinate(-1, -1) });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ctor_InvalidCoordinateFormat()
        {
            VehicleTracker trk = new VehicleTracker(new GeoCoordinate[] { new GeoCoordinate(-1000, -1000) });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "minSpeed must be positive and less than maxSpeed")]
        public void Ctor_InvalidCustomValues()
        {
            VehicleTracker trk = new VehicleTracker(new GeoCoordinate[] { new GeoCoordinate(-40, -40), new GeoCoordinate(-40, -40) },
                -1,
                15,
                5, 5, 5, 5);
        }

        [TestMethod]
        public void Tracker_FiresEventsAndUpdatesCoords()
        {
            GeoCoordinate startPoint = new GeoCoordinate(-40, -40);
            GeoCoordinate endPoint = new GeoCoordinate(-40.00001, -40.00001);

            MockConsumer mck = new MockConsumer();
            VehicleTracker trk = new VehicleTracker(new GeoCoordinate[] { startPoint, endPoint });

            mck.Subscribe(trk);
            trk.StartTracker();

            Assert.AreEqual(mck.latitude, endPoint.Latitude);
        }
    }
}
