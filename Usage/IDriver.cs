using System;

namespace DiscreteEventSimulator.Usage
{
    internal interface IDriver
    {
        void Move(int position, TimeSpan duration);

        double Position { get; }
    }
}
