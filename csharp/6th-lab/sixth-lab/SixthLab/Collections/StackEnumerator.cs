using System;
using System.Collections;
using System.Collections.Generic;

namespace SixthLab.Collections
{
    public sealed class StackEnumerator<T> : IEnumerator<T>
    {
        private event EventHandler IterationFinished;

        private readonly T[] array;

        private int position;

        public StackEnumerator(T[] array, EventHandler handler)
        {
            this.array = array;
            position = this.array.Length;
            IterationFinished = handler;
        }

        public T Current { get; set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            IterationFinished.Invoke(this, EventArgs.Empty);
        }

        public bool MoveNext()
        {
            if (--position < 0)
            {
                return false;
            }
            else
            {
                Current = array[position];
                return true;
            }
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
