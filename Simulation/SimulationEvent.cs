using System;
using System.Threading;

namespace DiscreteEventSimulator.Simulation
{
    internal class SimulationEvent : IDisposable
    {
        private readonly ManualResetEventSlim _continueEvent = new ManualResetEventSlim();
        private readonly SimulationThreadInfo _thread;
        private bool _disposed = false;

        public TimeSpan Time
        {
            get;
        }

        public SimulationEvent(TimeSpan time, SimulationThreadInfo thread)
        {
            if (thread == null) throw new ArgumentNullException(nameof(thread));
            Time = time;
            _thread = thread;
        }

        public void Release()
        {
            _thread.AdvanceTo(Time);
            _continueEvent.Set();
        }

        public void Wait()
        {
            _continueEvent.Wait();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _continueEvent?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
