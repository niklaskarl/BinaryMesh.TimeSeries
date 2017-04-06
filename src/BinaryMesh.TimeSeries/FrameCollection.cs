// -----------------------------------------------------------------------
// <copyright file="FrameCollection.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.TimeSeries
{
    internal sealed class FrameCollection : IList<IFrame>, IReadOnlyList<IFrame>
    {
        private readonly IFrame[] _frames;

        internal FrameCollection(IFrame[] frames)
        {
            _frames = frames;
        }

        public int Count => _frames.Length;

        bool ICollection<IFrame>.IsReadOnly => true;

        public IFrame this[int index] => _frames[index];

        IFrame IList<IFrame>.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException();
        }

        public int IndexOf(IFrame frame)
        {
            return ((IList<IFrame>)_frames).IndexOf(frame);
        }

        public bool Contains(IFrame frame)
        {
            return IndexOf(frame) >= 0;
        }

        public IEnumerator<IFrame> GetEnumerator()
        {
            return ((IEnumerable<IFrame>)_frames).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<IFrame>.CopyTo(IFrame[] array, int arrayIndex)
        {
            ((ICollection<IFrame>)_frames).CopyTo(array, arrayIndex);
        }

        void IList<IFrame>.Insert(int index, IFrame item)
        {
            throw new NotSupportedException();
        }

        void ICollection<IFrame>.Add(IFrame item)
        {
            throw new NotSupportedException();
        }

        void IList<IFrame>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        bool ICollection<IFrame>.Remove(IFrame item)
        {
            throw new NotSupportedException();
        }

        void ICollection<IFrame>.Clear()
        {
            throw new NotSupportedException();
        }
    }
}
