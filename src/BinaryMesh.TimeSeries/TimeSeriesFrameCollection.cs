// -----------------------------------------------------------------------
// <copyright file="TimeSeriesFrameCollection.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.TimeSeries
{
    internal sealed class TimeSeriesFrameCollection : IList<ITimeSeriesFrame>, IReadOnlyList<ITimeSeriesFrame>
    {
        private readonly ITimeSeriesFrame[] _frames;

        internal TimeSeriesFrameCollection(ITimeSeriesFrame[] frames)
        {
            _frames = frames;
        }

        public int Count => _frames.Length;

        bool ICollection<ITimeSeriesFrame>.IsReadOnly => true;

        public ITimeSeriesFrame this[int index] => _frames[index];

        ITimeSeriesFrame IList<ITimeSeriesFrame>.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException();
        }

        public int IndexOf(ITimeSeriesFrame frame)
        {
            return ((IList<ITimeSeriesFrame>)_frames).IndexOf(frame);
        }

        public bool Contains(ITimeSeriesFrame frame)
        {
            return IndexOf(frame) >= 0;
        }

        public IEnumerator<ITimeSeriesFrame> GetEnumerator()
        {
            return ((IEnumerable<ITimeSeriesFrame>)_frames).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<ITimeSeriesFrame>.CopyTo(ITimeSeriesFrame[] array, int arrayIndex)
        {
            ((ICollection<ITimeSeriesFrame>)_frames).CopyTo(array, arrayIndex);
        }

        void IList<ITimeSeriesFrame>.Insert(int index, ITimeSeriesFrame item)
        {
            throw new NotSupportedException();
        }

        void ICollection<ITimeSeriesFrame>.Add(ITimeSeriesFrame item)
        {
            throw new NotSupportedException();
        }

        void IList<ITimeSeriesFrame>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        bool ICollection<ITimeSeriesFrame>.Remove(ITimeSeriesFrame item)
        {
            throw new NotSupportedException();
        }

        void ICollection<ITimeSeriesFrame>.Clear()
        {
            throw new NotSupportedException();
        }
    }
}
