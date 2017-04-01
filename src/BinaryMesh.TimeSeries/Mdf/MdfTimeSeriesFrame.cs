// -----------------------------------------------------------------------
// <copyright file="MdfTimeSeriesFrame.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using BinaryMesh.Data.Mdf;

namespace BinaryMesh.TimeSeries.Mdf
{
    internal class MdfTimeSeriesFrame : ITimeSeriesFrame
    {
        private readonly MdfTimeSeriesSet _set;

        private readonly MdfChannelGroup _group;

        private readonly TimeSeriesSignalCollection _signals;

        internal MdfTimeSeriesFrame(MdfTimeSeriesSet set, MdfChannelGroup group)
        {
            _set = set;
            _group = group;
            _signals = new TimeSeriesSignalCollection(_group.Channels.Select(c => new MdfTimeSeriesSignal(this, c)).ToArray());
        }

        public ITimeSeriesSet Set => _set;

        public ITimeSeriesSignalCollection Signals => _signals;

        public long RecordCount => _group.Records.Count;

        public bool CanSeek => false;

        public ITimeSeriesRecordReader GetRecordReader()
        {
            return new MdfTimeSeriesRecordReader(this);
        }
    }
}
