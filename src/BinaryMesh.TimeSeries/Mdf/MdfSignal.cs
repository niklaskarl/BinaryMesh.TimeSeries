// -----------------------------------------------------------------------
// <copyright file="MdfSignal.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using BinaryMesh.Data.Mdf;
using BinaryMesh.TimeSeries.Common;

namespace BinaryMesh.TimeSeries.Mdf
{
    internal sealed class MdfSignal : IFrameSignal
    {
        private readonly MdfFrame _frame;

        private readonly int _index;

        private readonly MdfChannel _channel;

        private readonly SignalType _signalType;

        internal MdfSignal(MdfFrame frame, int index, MdfChannel channel)
        {
            _frame = frame;
            _index = index;
            _channel = channel;
            _signalType = GetSignalTypeForMdfDataType(channel.DataType);
        }

        public SignalType SignalType => _signalType;

        public IFrame Frame => _frame;

        public int Index => _index;

        public string Name => _channel.SignalName;

        public string DisplayName => string.IsNullOrWhiteSpace(_channel.DisplayName) ? _channel.SignalName : _channel.DisplayName;

        public double StartTime => _frame.StartTime;

        public double Duration => _frame.Duration;

        public bool HasAbsoluteTime => true;

        public DateTime AbsoluteStartTime => _frame.AbsoluteStartTime;

        internal MdfChannel Channel => _channel;

        public ISignalReader GetReader(double startTime)
        {
            switch (_signalType)
            {
                case SignalType.Real:
                    return new RealFrameSignalReader(this, startTime);
                default:
                    throw new NotSupportedException();
            }
        }

        private static SignalType GetSignalTypeForMdfDataType(MdfDataType type)
        {
            switch (type)
            {
                case MdfDataType.Integer:
                case MdfDataType.UnsignedInteger:
                case MdfDataType.FloatingPoint:
                    return SignalType.Real;
                case MdfDataType.String:
                    return SignalType.String;
                default:
                    return SignalType.Unknown;
            }
        }
    }
}
