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
        private readonly MdfTimeSeries _timeSeries;

        private readonly MdfChannelGroup _channelGroup;

        private readonly FrameSignalCollection _signals;

        private TimeSpan _startTime;

        private TimeSpan _duration;

        internal MdfFrame(MdfTimeSeries timeSeries, MdfChannelGroup channelGroup)
        {
            _timeSeries = timeSeries;
            _channelGroup = channelGroup;
            _signals = new FrameSignalCollection(_channelGroup.Channels.Select((c, i) => new MdfSignal(this, i, c)).ToArray());

            InitializeStartAndDuration();
        }

        public ITimeSeries TimeSeries => _timeSeries;

        public string Name => _channelGroup.Comment;

        public IFrameSignalCollection Signals => _signals;

        public long RecordCount => _channelGroup.Records.Count;

        public bool CanSeek => false;

        internal MdfChannelGroup ChannelGroup => _channelGroup;

        internal TimeSpan StartTime => _startTime;

        internal TimeSpan Duration => _duration;

        internal DateTime AbsoluteStartTime => _channelGroup.File.TimeStamp + StartTime;

        public IFrameReader GetReader()
        {
            return new MdfFrameReader(this);
        }

        private void InitializeStartAndDuration()
        {
            _startTime = TimeSpan.Zero;
            _duration = TimeSpan.Zero;

            MdfChannel timeChannel = _channelGroup.TimeChannel;
            MdfChannelConversion timeConversion = timeChannel.Conversion;
            if (timeConversion != null && timeConversion.MinimumPhysicalSignalValue.HasValue && timeConversion.MaximumPhysicalSignalValue.HasValue)
            {
                _startTime = TimeSpan.FromSeconds(timeConversion.MinimumPhysicalSignalValue.Value);
                _duration = TimeSpan.FromSeconds(timeConversion.MaximumPhysicalSignalValue.Value) - _startTime;
            }
            else if (timeChannel.MinimumRawSignalValue.HasValue && timeChannel.MaximumRawSignalValue.HasValue)
            {
                _startTime = TimeSpan.FromSeconds(timeChannel.MinimumRawSignalValue.Value);
                _duration = TimeSpan.FromSeconds(timeChannel.MaximumRawSignalValue.Value) - _startTime;
            }
            else
            {
                MdfRecordReader reader = _channelGroup.GetRecordReader();
                if (reader.Read())
                {
                    _startTime = TimeSpan.FromSeconds((double)reader.GetValue(timeChannel));

                    // seek to end of reader
                    if (reader.CanSeek)
                    {
                        reader.Seek(_channelGroup.Records.Count - 1);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                        }
                    }

                    _duration = TimeSpan.FromSeconds((double)reader.GetValue(timeChannel)) - _startTime;
                }
            }
        }
    }
}
