using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscreteEventSimulator
{
    public class PriorityQueue<TKey, TValue> : IPriorityQueue<TKey, TValue>
    {
        private readonly SortedDictionary<TKey, TValue> _PriorityQueue;

        public PriorityQueue()
        {
            _PriorityQueue = new SortedDictionary<TKey, TValue>();
        }

        //Adds the specified element with associated priority to the PriorityQueue<TKey,TValue>.
        public void Push(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (!_PriorityQueue.ContainsKey(key))
            {
                _PriorityQueue.Add(key, value);
            }
        }
        //Returns True if PriorityQueue is Empty
        public bool IsEmpty
        {
            get => EmptyQueue();
        }
        public bool EmptyQueue()
        {
            bool isEmpty = !_PriorityQueue.Any();
            return isEmpty;
        }

        //Returns the minimal element key from the PriorityQueue<TKey,TValue> without removing it.
        public TKey Peek()
        {
            if (_PriorityQueue.Count == 0)
                throw new InvalidOperationException("PriorityQueue<TKey, TValue> is empty.");

            return _PriorityQueue.First().Key;

        }
        /***Removes and returns the minimal element from the PriorityQueue<TKey,TValue> - 
         that is, the element with the lowest priority value.***/
        public KeyValuePair<TKey, TValue> Pop()
        {
            if (_PriorityQueue.Count == 0)
                throw new InvalidOperationException("PriorityQueue<TKey, TValue> is empty.");

            KeyValuePair<TKey, TValue> pair = _PriorityQueue.First();
            _PriorityQueue.Remove(pair.Key);
            return pair;
        }
        /***Removes the minimal element from the PriorityQueue<TElement,TPriority>, 
        and copies it and its associated priority to the element and priority arguments.***/
        public bool TryPop(out KeyValuePair<TKey, TValue> value)
        {
            if (_PriorityQueue.Count == 0)
            {
                value = default(KeyValuePair<TKey, TValue>);
                return false;
            }

            value = _PriorityQueue.First();
            return _PriorityQueue.Remove(value.Key);
        }
    }
}
