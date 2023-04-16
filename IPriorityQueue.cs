using System.Collections.Generic;

namespace DiscreteEventSimulator
{
    public interface IPriorityQueue<TKey, TValue>
    {
        void Push(TKey key, TValue value);

        bool IsEmpty { get; }

        TKey Peek();

        KeyValuePair<TKey, TValue> Pop();

        bool TryPop(out KeyValuePair<TKey, TValue> value);
    }
}
