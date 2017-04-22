// -----------------------------------------------------------------------
// <copyright file="MdfFrame.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using BinaryMesh.Data.Mdf;
using BinaryMesh.TimeSeries.Common;

namespace BinaryMesh.TimeSeries.Mdf
{
    internal sealed class MdfFrame : IFrame
    {
        private readonly MdfTimeSeries _timeSeries;

        private readonly MdfChannelGroup _channelGroup;

        private readonly FrameSignalCollection _signals;

        private double _startTime;

        private double _duration;

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

        internal double StartTime => _startTime;

        internal double Duration => _duration;

        internal DateTime AbsoluteStartTime => _channelGroup.File.TimeStamp + TimeSpan.FromTicks((long)(TimeSpan.TicksPerSecond * StartTime));

        public IDiscreteFrameReader GetDiscreteReader()
        {
            return new MdfFrameReader(this);
        }

        public IContinuousFrameReader GetContinuousReader(double startTime)
        {
            return new ContinuousFrameReader(this, startTime);
        }

        private void InitializeStartAndDuration()
        {
            _startTime = 0.0;
            _duration = 0.0;

            MdfChannel timeChannel = _channelGroup.TimeChannel;
            MdfChannelConversion timeConversion = timeChannel.Conversion;
            if (timeConversion != null && timeConversion.MinimumPhysicalSignalValue.HasValue && timeConversion.MaximumPhysicalSignalValue.HasValue)
            {
                _startTime = timeConversion.MinimumPhysicalSignalValue.Value;
                _duration = timeConversion.MaximumPhysicalSignalValue.Value - _startTime;
            }
            else if (timeChannel.MinimumRawSignalValue.HasValue && timeChannel.MaximumRawSignalValue.HasValue)
            {
                _startTime = timeChannel.MinimumRawSignalValue.Value;
                _duration = timeChannel.MaximumRawSignalValue.Value - _startTime;
            }
            else
            {
                MdfRecordReader reader = _channelGroup.GetRecordReader();
                if (reader.Read())
                {
                    _startTime = (double)reader.GetValue(timeChannel);

                    // seek to end of reader
                    if (reader.CanSeek)
                    {
                        reader.Seek(_channelGroup.Records.Count - 1);
                    }
                    else
                    {
                        int i = 1;
                        while (i < _channelGroup.Records.Count && reader.Read())
                        {
                            i++;
                        }
                    }

                    _duration = (double)reader.GetValue(timeChannel) - _startTime;
                }
            }
        }
    }
}
