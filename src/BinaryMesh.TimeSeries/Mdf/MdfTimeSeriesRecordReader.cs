// -----------------------------------------------------------------------
// <copyright file="MdfTimeSeriesRecordReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries.Mdf
{
    internal class MdfTimeSeriesRecordReader : ITimeSeriesRecordReader
    {
        private readonly MdfTimeSeriesFrame _frame;

        internal MdfTimeSeriesRecordReader(MdfTimeSeriesFrame frame)
        {
            _frame = frame;
        }

        public long CurrentIndex => throw new NotImplementedException();

        public TimeSpan CurrentOffset => throw new NotImplementedException();

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public void Seek(long index)
        {
            throw new NotImplementedException();
        }

        public void Seek(TimeSpan offset, SeekMode mode)
        {
            throw new NotImplementedException();
        }
    }
}
