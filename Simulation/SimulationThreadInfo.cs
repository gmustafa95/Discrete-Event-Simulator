using System;
using System.Diagnostics;

namespace DiscreteEventSimulator.Simulation
{
    internal class SimulationThreadInfo
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public TimeSpan Time
        {
            get;
            private set;
        }

        public TimeSpan GetCurrentTime()
        {
            return Time + _stopwatch.Elapsed;
        }

        public void Initialize()
        {
            _stopwatch.Start();
        }

        public void Delay(Simulator simulator, TimeSpan delay)
        {
            _stopwatch.Stop();
            var endTime = Time + _stopwatch.Elapsed + delay;
            Time = endTime;
            var ev = new SimulationEvent(endTime, this);
            simulator.PushEvent(ev);
            simulator.TryAdvanceSimulationTime();
            ev.Wait();
            _stopwatch.Restart();
        }

        public void AdvanceTo(TimeSpan time)
        {
            Time = time;
        }
    }
}
