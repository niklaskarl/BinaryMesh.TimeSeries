// -----------------------------------------------------------------------
// <copyright file="MdfFrame.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using BinaryMesh.Data.Mdf;

namespace BinaryMesh.TimeSeries.Mdf
{
    internal sealed class MdfFrame : IFrame
    {
        private readonly MdfTimeSeries _set;

        private readonly MdfChannelGroup _channelGroup;

        private readonly FrameSignalCollection _signals;

        internal MdfFrame(MdfTimeSeries set, MdfChannelGroup channelGroup)
        {
            _set = set;
            _channelGroup = channelGroup;
            _signals = new FrameSignalCollection(_channelGroup.Channels.Select((c, i) => new MdfSignal(this, i, c)).ToArray());
        }

        public ITimeSeries Set => _set;

        public string Name => _channelGroup.Comment;

        public DateTime StartTime => _channelGroup.File.TimeStamp;

        public IFrameSignalCollection Signals => _signals;

        public long RecordCount => _channelGroup.Records.Count;

        public bool CanSeek => false;

        internal MdfChannelGroup ChannelGroup => _channelGroup;

        public IFrameReader GetDiscreteReader()
        {
            return new MdfFrameReader(this);
        }
    }
}
