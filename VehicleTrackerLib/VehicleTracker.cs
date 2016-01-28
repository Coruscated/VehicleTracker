using System;
using System.Diagnostics;
using System.Device.Location;
using System.Threading;

namespace VehicleTrackerLib
{

    public class VehicleTracker
    {
        private GeoCoordinate _startPosition;
        private GeoCoordinate _endPosition;
        private GeoCoordinate _currentPosition;
        private GeoCoordinate[] _waypoints;

        private Random _random;

        private double _minSpeed;
        private double _maxSpeed;
        private double _speedBetweenPosTolerance;
        private double _speedInMph;

        private int _lowSpeedDelayInSeconds;
        private int _midSpeedDelayInSeconds;
        private int _highSpeedDelayInSeconds;

        public VehicleTracker(GeoCoordinate[] waypoints) :
            // Load with defaults
            this(waypoints, 0, 75, 9, 6, 3, 10)
        { }

        public VehicleTracker(GeoCoordinate[] waypoints, double minSpeed, double maxSpeed,
            int lowSpeedDelayInSecs,
            int midSpeedDelayInSecs,
            int highSpeedDelayInSecs,
            double speedBetweenPosTolerance
            )
        {
            if (waypoints.Length < 2)
            {
                throw new ArgumentException("Waypoints must be a GeoCoordinate array of at least 2");
            }
            else if (minSpeed < 0 || minSpeed >= maxSpeed)
            {
                throw new ArgumentException("minSpeed must be positive and less than maxSpeed");
            }
            else if (speedBetweenPosTolerance <= 0)
            {
                throw new ArgumentException("Speed tolerance must be greater than zero");
            }
            else if (lowSpeedDelayInSecs <= 0 || midSpeedDelayInSecs <= 0 || highSpeedDelayInSecs <= 0)
            {
                throw new ArgumentException("Time delays must be positive");
            }

            _waypoints = waypoints;
            _minSpeed = minSpeed;
            _maxSpeed = maxSpeed;
            _lowSpeedDelayInSeconds = lowSpeedDelayInSecs;
            _midSpeedDelayInSeconds = midSpeedDelayInSecs;
            _highSpeedDelayInSeconds = highSpeedDelayInSecs;
            _speedBetweenPosTolerance = speedBetweenPosTolerance;
            _random = new Random();
            _speedInMph = _random.NextDouble(_minSpeed, _maxSpeed);
        }

        public delegate void PositionChangeHandler(
           object tracker,
           double latitude,
           double longitude
        );

        public event PositionChangeHandler PositionChange;

        protected void OnPositionChange(
           object tracker,
           double latitude,
           double longitude
        )
        {
            // Subscriber check
            if (PositionChange != null)
            {
                PositionChange(tracker, latitude, longitude);
            }
        }

        public void StartTracker()
        {

            for (int i = 1; i < _waypoints.Length; i++)
            {
                _startPosition = _waypoints[i - 1];
                _endPosition = _waypoints[i];
                _currentPosition = _startPosition;

                double distTravelledThisWaypoint = 0;
                double distToNextWaypoint = _startPosition.GetDistanceTo(_endPosition);
                bool destReached = false;

                while (!destReached)
                {
                    _speedInMph = _random.NextDouble(
                        Math.Max(_minSpeed, _speedInMph - _speedBetweenPosTolerance),
                        Math.Min(_maxSpeed, _speedInMph + _speedBetweenPosTolerance)
                        );

                    int delayInSeconds;
                    if (_speedInMph >= 0 && _speedInMph < 25)
                    {
                        delayInSeconds = _lowSpeedDelayInSeconds;
                    }
                    else if (_speedInMph >= 25 && _speedInMph < 50)
                    {
                        delayInSeconds = _midSpeedDelayInSeconds;
                    }
                    else
                    {
                        delayInSeconds = _highSpeedDelayInSeconds;
                    }

                    Trace.TraceInformation("Current speed: {0} MPH", _speedInMph);
                    Trace.TraceInformation("Current GPS Tracker delay: {0} seconds", delayInSeconds);
                    Trace.TraceInformation("Current Position: {0}, {1}", _currentPosition.Latitude, _currentPosition.Longitude);

                    Thread.Sleep(new TimeSpan(0, 0, delayInSeconds));

                    double distanceTravelledInMiles = ((_speedInMph / 60) / 60) * delayInSeconds;
                    double distanceTravelledInMeters = GPSMath.milesToMeters(distanceTravelledInMiles);
                    distTravelledThisWaypoint += distanceTravelledInMeters;

                    if (distTravelledThisWaypoint <= distToNextWaypoint)
                    {
                        _currentPosition = GPSCalc.moveTowards(_currentPosition, _endPosition, distanceTravelledInMeters);
                    }
                    else
                    {
                        _currentPosition = _endPosition;
                        destReached = true;
                    }

                    // We have moved & waited, notify subscribers
                    OnPositionChange(this, _currentPosition.Latitude, _currentPosition.Longitude);

                }
            }
        }


    }

}