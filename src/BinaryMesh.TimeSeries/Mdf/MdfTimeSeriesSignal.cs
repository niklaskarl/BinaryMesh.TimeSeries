// -----------------------------------------------------------------------
// <copyright file="MdfTimeSeriesSignal.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using BinaryMesh.Data.Mdf;

namespace BinaryMesh.TimeSeries.Mdf
{
    internal class MdfTimeSeriesSignal : ITimeSeriesSignal
    {
        private readonly MdfTimeSeriesFrame _frame;

        private readonly MdfChannel _channel;

        private readonly TimeSeriesSignalType _signalType;

        internal MdfTimeSeriesSignal(MdfTimeSeriesFrame frame, MdfChannel channel)
        {
            _frame = frame;
            _channel = channel;
            _signalType = GetSignalTypeForMdfDataType(channel.DataType);
        }

        public TimeSeriesSignalType SignalType => _signalType;

        public ITimeSeriesFrame Frame => _frame;

        public string Name => _channel.SignalName;

        public string DisplayName => _channel.DisplayName;

        internal MdfChannel Channel => _channel;

        private static TimeSeriesSignalType GetSignalTypeForMdfDataType(MdfDataType type)
        {
            switch (type)
            {
                case MdfDataType.Integer:
                case MdfDataType.UnsignedInteger:
                case MdfDataType.FloatingPoint:
                    return TimeSeriesSignalType.Real;
                case MdfDataType.String:
                    return TimeSeriesSignalType.String;
                default:
                    return TimeSeriesSignalType.Unknown;
            }
        }
    }
}
