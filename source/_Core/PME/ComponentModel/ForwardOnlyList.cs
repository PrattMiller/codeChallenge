using System;
using System.Collections;
using System.Collections.Generic;

namespace PME.ComponentModel
{
    public class ForwardOnlyList<T> : IEnumerable<T>
    {

        private readonly List<T> _list = new List<T>();
        private int _count = 0;
        private volatile object _lockObject = new object();
        
        private class ForwardOnlyListEnumerator : DisposableObject, IEnumerator<T>
        {
            private ForwardOnlyList<T> _list;
            private int _position = -1;

            public ForwardOnlyListEnumerator(ForwardOnlyList<T> composite)
            {
                _list = composite;
            }

            public override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
            }

            public object Current
            {
                get { return GetCurrent(); }
            }
            T IEnumerator<T>.Current
            {
                get { return GetCurrent(); }
            }

            private T GetCurrent()
            {
                return _list[_position];
            }

            public bool MoveNext()
            {
                _position++;

                return (_position < _list._count);
            }

            public void Reset()
            {
                _position = -1;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ForwardOnlyListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ForwardOnlyListEnumerator(this);
        }

        public T this[int index]
        {
            get { return _list[index]; }
        }

        public int Count
        {
            get { return _count; }
        }

        public virtual void Add(T item)
        {
            lock (_lockObject)
            {
                _list.Add(item);
                _count++;
            }
        }

        public virtual void Clear()
        {
            lock (_lockObject)
            {
                _list.Clear();
                _count = 0;
            }
        }
        

    }
}

