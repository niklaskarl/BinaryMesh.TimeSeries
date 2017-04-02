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

        private readonly MdfChannelGroup _channelGroup;

        private readonly TimeSeriesSignalCollection _signals;

        internal MdfTimeSeriesFrame(MdfTimeSeriesSet set, MdfChannelGroup channelGroup)
        {
            _set = set;
            _channelGroup = channelGroup;
            _signals = new TimeSeriesSignalCollection(_channelGroup.Channels.Select(c => new MdfTimeSeriesSignal(this, c)).ToArray());
        }

        public ITimeSeriesSet Set => _set;

        public string Name => _channelGroup.Comment;

        public ITimeSeriesSignalCollection Signals => _signals;

        public long RecordCount => _channelGroup.Records.Count;

        public bool CanSeek => false;

        internal MdfChannelGroup ChannelGroup => _channelGroup;

        public ITimeSeriesRecordReader GetRecordReader()
        {
            return new MdfTimeSeriesRecordReader(this);
        }
    }
}
