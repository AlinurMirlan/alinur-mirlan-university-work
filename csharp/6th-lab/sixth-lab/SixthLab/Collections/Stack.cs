using System;
using System.Collections;
using System.Collections.Generic;

namespace SixthLab.Collections
{
    public class Stack<T> : IEnumerable<T>
    {
        private readonly object _lock = new();

        public Stack()
        {
            array = new T[capacity];
        }

        public Stack(int capacity)
        {
            array = new T[capacity];
        }

        public Stack(IEnumerable<T> collection)
        {
            array = new T[capacity];
            foreach (T item in collection)
            {
                if (++topIndex >= capacity)
                {
                    IncreaseCapacity();
                }
                array[topIndex] = item;
                count += 1;
            }
        }

        private const int growthRate = 3;

        private const int initialCapacity = 10;

        private T[] array;

        private int topIndex = -1;

        private int count = 0;
        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return count;
                }
            }
        }

        private int capacity = initialCapacity;

        private bool isBeingIterated = false;

        public bool TryPop(out T value)
        {
            lock (_lock)
            {
                CheckIfIsBeingIterated();
                if (Count <= 0)
                {
                    value = default;
                    return false;
                }

                T topItem = array[topIndex];
                array[topIndex--] = default;
                count--;
                value = topItem;
                return true;
            }
        }

        public T Peek()
        {
            lock (_lock)
            {
                if (Count <= 0)
                {
                    throw new InvalidOperationException("Stack is empty");
                }
                return array[topIndex];
            }
        }

        public void Push(T item)
        {
            lock (_lock)
            {
                CheckIfIsBeingIterated();
                if (++topIndex >= capacity)
                {
                    IncreaseCapacity();
                }
                array[topIndex] = item;
                count += 1;
            }
        }

        public T[] ToArray()
        {
            lock (_lock)
            {
                return array[..count];
            }
        }

        public bool Contains(T item)
        {
            lock (_lock)
            {
                for (int i = 0; i < count; i++)
                {
                    if (item is null)
                    {
                        if (array[i] is null)
                            return true;
                    }
                    else if (item.Equals(array[i]))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                count = 0;
                topIndex = 0;
                capacity = initialCapacity;
                array = new T[capacity];
            }
        }
        private void IncreaseCapacity()
        {
            capacity *= growthRate;
            T[] newArray = new T[capacity];
            array.CopyTo(newArray, 0);
            array = newArray;
        }

        private void CheckIfIsBeingIterated()
        {
            if (isBeingIterated)
            {
                isBeingIterated = false;
                throw new InvalidOperationException("Stack cannot be changed during iteration");
            }
        }

        private void OnIterationFinish(object? sender, EventArgs args)
        {
            isBeingIterated = false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            isBeingIterated = true;
            var enumerator = new StackEnumerator<T>(array[..count], OnIterationFinish);
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}