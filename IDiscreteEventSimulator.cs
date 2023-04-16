using System;
using System.Threading;

namespace DiscreteEventSimulator
{
    public interface IDiscreteEventSimulator
    {
        TimeSpan GetTime();

        TimeSpan GetTime(Thread thread);

        void AddThread(Thread thread);

        void RemoveThread();

        void Delay(TimeSpan delay);

        void Delay(TimeSpan delay, Thread thread);

        void Start();

        TimeSpan Stop();
    }
}
