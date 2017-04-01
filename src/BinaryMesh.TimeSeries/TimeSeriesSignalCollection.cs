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
        private readonly ITimeSeriesSignal[] _columns;

        internal TimeSeriesSignalCollection(ITimeSeriesSignal[] columns)
        {
            _columns = columns;
        }

        public int Count => _columns.Length;

        bool ICollection<KeyValuePair<string, ITimeSeriesSignal>>.IsReadOnly => true;

        bool ICollection<ITimeSeriesSignal>.IsReadOnly => true;

        public ICollection<string> Keys => _columns.Select(c => c.Name).ToList();

        IEnumerable<string> IReadOnlyDictionary<string, ITimeSeriesSignal>.Keys => Keys;

        public ICollection<ITimeSeriesSignal> Values => this;

        IEnumerable<ITimeSeriesSignal> IReadOnlyDictionary<string, ITimeSeriesSignal>.Values => Values;

        public ITimeSeriesSignal this[int index] => _columns[index];

        public ITimeSeriesSignal this[string columnName] => _columns.FirstOrDefault(c => c.Name == columnName) ?? throw new KeyNotFoundException();

        ITimeSeriesSignal IList<ITimeSeriesSignal>.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException();
        }

        ITimeSeriesSignal IDictionary<string, ITimeSeriesSignal>.this[string columnName]
        {
            get => this[columnName];
            set => throw new NotSupportedException();
        }

        public bool TryGetValue(string columnName, out ITimeSeriesSignal value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == columnName)
                {
                    value = this[i];
                    return true;
                }
            }

            value = null;
            return false;
        }

        public int IndexOf(ITimeSeriesSignal column)
        {
            return ((IList<ITimeSeriesSignal>)_columns).IndexOf(column);
        }

        public bool Contains(ITimeSeriesSignal column)
        {
            return IndexOf(column) >= 0;
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

        public bool ContainsKey(string columnName)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == columnName)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<ITimeSeriesSignal> GetEnumerator()
        {
            return (_columns as IEnumerable<ITimeSeriesSignal>).GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, ITimeSeriesSignal>> IEnumerable<KeyValuePair<string, ITimeSeriesSignal>>.GetEnumerator()
        {
            return _columns.Select(c => new KeyValuePair<string, ITimeSeriesSignal>(c.Name, c)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<ITimeSeriesSignal>.CopyTo(ITimeSeriesSignal[] array, int arrayIndex)
        {
            ((ICollection<ITimeSeriesSignal>)_columns).CopyTo(array, arrayIndex);
        }

        void ICollection<KeyValuePair<string, ITimeSeriesSignal>>.CopyTo(KeyValuePair<string, ITimeSeriesSignal>[] array, int arrayIndex)
        {
            _columns.Select(c => new KeyValuePair<string, ITimeSeriesSignal>(c.Name, c)).ToList().CopyTo(array, arrayIndex);
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
