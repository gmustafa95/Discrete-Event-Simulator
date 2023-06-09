﻿using System;
using System.Threading;

namespace DiscreteEventSimulator.Simulation
{
    internal class SimulationEvent
    {
        private readonly ManualResetEventSlim _continueEvent = new ManualResetEventSlim();
        private readonly SimulationThreadInfo _thread;

        public TimeSpan Time
        {
            get;
        }

        public SimulationEvent(TimeSpan time, SimulationThreadInfo thread)
        {
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
    }
}
