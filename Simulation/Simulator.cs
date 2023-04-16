using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DiscreteEventSimulator.Simulation
{
    public class Simulator : IDiscreteEventSimulator
    {
        private readonly Dictionary<Thread, SimulationThreadInfo> _threads = new Dictionary<Thread, SimulationThreadInfo>();
        private readonly PriorityQueue<TimeSpan, SimulationEvent> _timeLine = new PriorityQueue<TimeSpan, SimulationEvent>();
        private readonly ManualResetEventSlim _completionEvent = new ManualResetEventSlim();
        private readonly object _lock = new object();
        public TimeSpan Time
        {
            get;
            private set;
        }

        public void AddThread(Thread thread)
        {
            _threads.Add(thread, new SimulationThreadInfo());
        }

        public void Delay(TimeSpan delay)
        {
            Delay(delay, Thread.CurrentThread);
        }

        public void Delay(TimeSpan delay, Thread thread)
        {
            _threads[thread].Delay(this, delay);
        }

        internal void TryAdvanceSimulationTime()
        {
            lock (_lock)
            {
                if (_threads.Count > 0)
                {
                    var nextTime = _threads.Values.Min(t => t.Time);
                    while ((!_timeLine.IsEmpty) && (_timeLine.Peek() <= nextTime))
                    {
                        var nextEvent = _timeLine.Pop();
                        Time = nextEvent.Key;
                        nextEvent.Value.Release();
                    }
                    if (_timeLine.IsEmpty)
                    {
                        _completionEvent.Set();
                    }
                }
            }
        }

        internal void AdvanceSimulationTime()
        {
            while (!_timeLine.IsEmpty)
            {
                var nextEvent = _timeLine.Pop();
                Time = nextEvent.Key;
                nextEvent.Value.Release();
            }
        }

        public TimeSpan GetTime()
        {
            return GetTime(Thread.CurrentThread);
        }

        public TimeSpan GetTime(Thread thread)
        {
            return _threads[thread].GetCurrentTime();
        }

        public void RemoveThread()
        {
            _threads.Remove(Thread.CurrentThread);
            TryAdvanceSimulationTime();
        }

        public void Start()
        {
            foreach (var thread in _threads.Values)
            {
                thread.Initialize();
            }
        }

        public TimeSpan Stop()
        {
            while (!_timeLine.IsEmpty)
            {
                _completionEvent.Wait();
            }
            return Time;
        }

        internal void PushEvent(SimulationEvent ev)
        {
            lock (_lock)
            {
                _timeLine.Push(ev.Time, ev);
            }
        }
    }
}
