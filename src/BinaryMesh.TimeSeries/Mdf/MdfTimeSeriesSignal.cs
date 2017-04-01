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
        private MdfTimeSeriesFrame _frame;

        private MdfChannel _channel;

        internal MdfTimeSeriesSignal(MdfTimeSeriesFrame frame, MdfChannel channel)
        {
            _frame = frame;
            _channel = channel;
        }

        public ITimeSeriesFrame Frame => _frame;

        public string Name => _channel.SignalName;

        public string DisplayName => _channel.DisplayName;
    }
}
