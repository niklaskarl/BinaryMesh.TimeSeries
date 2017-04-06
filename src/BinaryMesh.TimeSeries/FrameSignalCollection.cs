// -----------------------------------------------------------------------
// <copyright file="FrameSignalCollection.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryMesh.TimeSeries
{
    internal sealed class FrameSignalCollection :
        IFrameSignalCollection,
        IList<IFrameSignal>, IReadOnlyList<IFrameSignal>,
        IDictionary<string, IFrameSignal>, IReadOnlyDictionary<string, IFrameSignal>
    {
        private readonly IFrameSignal[] _signals;

        internal FrameSignalCollection(IFrameSignal[] signals)
        {
            _signals = signals;
        }

        public int Count => _signals.Length;

        bool ICollection<KeyValuePair<string, IFrameSignal>>.IsReadOnly => true;

        bool ICollection<IFrameSignal>.IsReadOnly => true;

        public IEnumerable<string> Keys => _signals.Select(c => c.Name);

        ICollection<string> IDictionary<string, IFrameSignal>.Keys => Keys.ToList();

        public ICollection<IFrameSignal> Values => this;

        IEnumerable<IFrameSignal> IReadOnlyDictionary<string, IFrameSignal>.Values => Values;

        public IFrameSignal this[int index] => _signals[index];

        public IFrameSignal this[string signalName] => _signals.FirstOrDefault(c => c.Name == signalName) ?? throw new KeyNotFoundException();

        IFrameSignal IList<IFrameSignal>.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException();
        }

        IFrameSignal IDictionary<string, IFrameSignal>.this[string signalName]
        {
            get => this[signalName];
            set => throw new NotSupportedException();
        }

        public bool TryGetValue(string signalName, out IFrameSignal value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == signalName)
                {
                    value = this[i];
                    return true;
                }
            }

            value = null;
            return false;
        }

        public int IndexOf(IFrameSignal signal)
        {
            return ((IList<IFrameSignal>)_signals).IndexOf(signal);
        }

        public bool Contains(IFrameSignal signal)
        {
            return IndexOf(signal) >= 0;
        }

        bool ICollection<KeyValuePair<string, IFrameSignal>>.Contains(KeyValuePair<string, IFrameSignal> item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == item.Key && this[i] == item.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKey(string signalName)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == signalName)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<IFrameSignal> GetEnumerator()
        {
            return (_signals as IEnumerable<IFrameSignal>).GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, IFrameSignal>> IEnumerable<KeyValuePair<string, IFrameSignal>>.GetEnumerator()
        {
            return _signals.Select(c => new KeyValuePair<string, IFrameSignal>(c.Name, c)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<IFrameSignal>.CopyTo(IFrameSignal[] array, int arrayIndex)
        {
            ((ICollection<IFrameSignal>)_signals).CopyTo(array, arrayIndex);
        }

        void ICollection<KeyValuePair<string, IFrameSignal>>.CopyTo(KeyValuePair<string, IFrameSignal>[] array, int arrayIndex)
        {
            _signals.Select(c => new KeyValuePair<string, IFrameSignal>(c.Name, c)).ToList().CopyTo(array, arrayIndex);
        }

        void IList<IFrameSignal>.Insert(int index, IFrameSignal item)
        {
            throw new NotSupportedException();
        }

        void ICollection<IFrameSignal>.Add(IFrameSignal item)
        {
            throw new NotSupportedException();
        }

        void IDictionary<string, IFrameSignal>.Add(string key, IFrameSignal value)
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<string, IFrameSignal>>.Add(KeyValuePair<string, IFrameSignal> item)
        {
            throw new NotSupportedException();
        }

        void IList<IFrameSignal>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        bool ICollection<IFrameSignal>.Remove(IFrameSignal item)
        {
            throw new NotSupportedException();
        }

        bool IDictionary<string, IFrameSignal>.Remove(string key)
        {
            throw new NotSupportedException();
        }

        bool ICollection<KeyValuePair<string, IFrameSignal>>.Remove(KeyValuePair<string, IFrameSignal> item)
        {
            throw new NotSupportedException();
        }

        void ICollection<IFrameSignal>.Clear()
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<string, IFrameSignal>>.Clear()
        {
            throw new NotSupportedException();
        }
    }
}
