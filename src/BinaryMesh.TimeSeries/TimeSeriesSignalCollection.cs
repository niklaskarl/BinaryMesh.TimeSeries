// -----------------------------------------------------------------------
// <copyright file="TimeSeriesSignalCollection.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryMesh.TimeSeries
{
    internal sealed class TimeSeriesSignalCollection :
        ITimeSeriesSignalCollection,
        IList<ITimeSeriesSignal>, IReadOnlyList<ITimeSeriesSignal>,
        IDictionary<string, ITimeSeriesSignal>, IReadOnlyDictionary<string, ITimeSeriesSignal>
    {
        private readonly ITimeSeriesSignal[] _signals;

        internal TimeSeriesSignalCollection(ITimeSeriesSignal[] signals)
        {
            _signals = signals;
        }

        public int Count => _signals.Length;

        bool ICollection<KeyValuePair<string, ITimeSeriesSignal>>.IsReadOnly => true;

        bool ICollection<ITimeSeriesSignal>.IsReadOnly => true;

        public ICollection<string> Keys => _signals.Select(c => c.Name).ToList();

        IEnumerable<string> IReadOnlyDictionary<string, ITimeSeriesSignal>.Keys => Keys;

        public ICollection<ITimeSeriesSignal> Values => this;

        IEnumerable<ITimeSeriesSignal> IReadOnlyDictionary<string, ITimeSeriesSignal>.Values => Values;

        public ITimeSeriesSignal this[int index] => _signals[index];

        public ITimeSeriesSignal this[string signalName] => _signals.FirstOrDefault(c => c.Name == signalName) ?? throw new KeyNotFoundException();

        ITimeSeriesSignal IList<ITimeSeriesSignal>.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException();
        }

        ITimeSeriesSignal IDictionary<string, ITimeSeriesSignal>.this[string signalName]
        {
            get => this[signalName];
            set => throw new NotSupportedException();
        }

        public bool TryGetValue(string signalName, out ITimeSeriesSignal value)
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

        public int IndexOf(ITimeSeriesSignal signal)
        {
            return ((IList<ITimeSeriesSignal>)_signals).IndexOf(signal);
        }

        public bool Contains(ITimeSeriesSignal signal)
        {
            return IndexOf(signal) >= 0;
        }

        bool ICollection<KeyValuePair<string, ITimeSeriesSignal>>.Contains(KeyValuePair<string, ITimeSeriesSignal> item)
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

        public IEnumerator<ITimeSeriesSignal> GetEnumerator()
        {
            return (_signals as IEnumerable<ITimeSeriesSignal>).GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, ITimeSeriesSignal>> IEnumerable<KeyValuePair<string, ITimeSeriesSignal>>.GetEnumerator()
        {
            return _signals.Select(c => new KeyValuePair<string, ITimeSeriesSignal>(c.Name, c)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<ITimeSeriesSignal>.CopyTo(ITimeSeriesSignal[] array, int arrayIndex)
        {
            ((ICollection<ITimeSeriesSignal>)_signals).CopyTo(array, arrayIndex);
        }

        void ICollection<KeyValuePair<string, ITimeSeriesSignal>>.CopyTo(KeyValuePair<string, ITimeSeriesSignal>[] array, int arrayIndex)
        {
            _signals.Select(c => new KeyValuePair<string, ITimeSeriesSignal>(c.Name, c)).ToList().CopyTo(array, arrayIndex);
        }

        void IList<ITimeSeriesSignal>.Insert(int index, ITimeSeriesSignal item)
        {
            throw new NotSupportedException();
        }

        void ICollection<ITimeSeriesSignal>.Add(ITimeSeriesSignal item)
        {
            throw new NotSupportedException();
        }

        void IDictionary<string, ITimeSeriesSignal>.Add(string key, ITimeSeriesSignal value)
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<string, ITimeSeriesSignal>>.Add(KeyValuePair<string, ITimeSeriesSignal> item)
        {
            throw new NotSupportedException();
        }

        void IList<ITimeSeriesSignal>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        bool ICollection<ITimeSeriesSignal>.Remove(ITimeSeriesSignal item)
        {
            throw new NotSupportedException();
        }

        bool IDictionary<string, ITimeSeriesSignal>.Remove(string key)
        {
            throw new NotSupportedException();
        }

        bool ICollection<KeyValuePair<string, ITimeSeriesSignal>>.Remove(KeyValuePair<string, ITimeSeriesSignal> item)
        {
            throw new NotSupportedException();
        }

        void ICollection<ITimeSeriesSignal>.Clear()
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<string, ITimeSeriesSignal>>.Clear()
        {
            throw new NotSupportedException();
        }
    }
}
