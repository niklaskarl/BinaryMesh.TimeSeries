// -----------------------------------------------------------------------
// <copyright file="RTimeSeriesFrame.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using BinaryMesh.Data.RLanguage;

namespace BinaryMesh.TimeSeries.RLanguage
{
    internal sealed class RTimeSeriesFrame : ITimeSeriesFrame
    {
        private readonly RTimeSeriesSet _set;

        private readonly RTimeSeriesTimeSignal _timeSignal;

        private readonly DataFrame _dataFrame;

        public RTimeSeriesFrame(RTimeSeriesSet set, RTimeSeriesTimeSignal timeSignal, DataFrame dataFrame)
        {
            _set = set;
            _timeSignal = timeSignal;
            _dataFrame = dataFrame;

            Signals = new TimeSeriesSignalCollection(_dataFrame.Columns.Values.Except(new DataFrameColumn[] { _timeSignal.Column }).Select(c => new RTimeSeriesSignal(this, c)).ToArray());
        }

        public ITimeSeriesSet Set => _set;

        public string Name => "All Signals";

        public ITimeSeriesSignalCollection Signals { get; }

        public long RecordCount => _dataFrame.RowCount;

        public bool CanSeek => true;

        internal RTimeSeriesTimeSignal TimeSignal => _timeSignal;

        public ITimeSeriesRecordReader GetRecordReader()
        {
            return new RTimeSeriesRecordReader(this);
        }
    }
}
