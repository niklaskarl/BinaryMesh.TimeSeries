// -----------------------------------------------------------------------
// <copyright file="RTimeSeriesSignal.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using BinaryMesh.Data.RLanguage;

namespace BinaryMesh.TimeSeries.RLanguage
{
    internal sealed class RTimeSeriesSignal : ITimeSeriesSignal
    {
        private readonly RTimeSeriesFrame _frame;

        private readonly DataFrameColumn _column;

        private readonly TimeSeriesSignalType _signalType;

        public RTimeSeriesSignal(RTimeSeriesFrame frame, DataFrameColumn column)
        {
            _frame = frame;
            _column = column;

            switch (column.Vector.Type)
            {
                case VectorType.Integer:
                    _signalType = TimeSeriesSignalType.Real;
                    break;
                case VectorType.Real:
                    _signalType = TimeSeriesSignalType.Real;
                    break;
                case VectorType.String:
                    _signalType = TimeSeriesSignalType.String;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public ITimeSeriesFrame Frame => _frame;

        public TimeSeriesSignalType SignalType => _signalType;

        public string Name => _column.Name;

        public string DisplayName => _column.Name;

        internal DataFrameColumn Column => _column;
    }
}
