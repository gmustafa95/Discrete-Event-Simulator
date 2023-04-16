using System;

namespace DiscreteEventSimulator.Usage
{
    internal class Driver : IDriver
    {
        private readonly IDiscreteEventSimulator _eventSimulator;
        private int _lastPosition;
        private TimeSpan _startTime;
        private int? _endPosition;
        private double _duration;

        public Driver(IDiscreteEventSimulator eventSimulator, int position)
        {
            _eventSimulator = eventSimulator;
            _lastPosition = position;
        }

        public double Position => CalculatePosition();

        private double CalculatePosition()
        {
            var target = _endPosition;
            if (!target.HasValue)
            {
                return _lastPosition;
            }

            var time = _eventSimulator.GetTime();
            var completed = (time - _startTime).TotalMilliseconds / _duration;
            return _lastPosition + completed * (target.Value - _lastPosition);
        }

        public void Move(int position, TimeSpan duration)
        {
            _duration = duration.TotalMilliseconds;
            _endPosition = position;
            _startTime = _eventSimulator.GetTime();
            _eventSimulator.Delay(duration);
            _lastPosition = position;
            _endPosition = null;
        }
    }
}
