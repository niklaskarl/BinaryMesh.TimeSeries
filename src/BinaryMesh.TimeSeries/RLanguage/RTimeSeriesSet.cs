// -----------------------------------------------------------------------
// <copyright file="RTimeSeriesSet.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using BinaryMesh.Data.RLanguage;

namespace BinaryMesh.TimeSeries.RLanguage
{
    internal sealed class RTimeSeriesSet : ITimeSeriesSet
    {
        internal RTimeSeriesSet(DataFrame dataFrame, RTimeSeriesTimeSignal timeSignal)
        {
            Frames = new TimeSeriesFrameCollection(new ITimeSeriesFrame[] { new RTimeSeriesFrame(this, timeSignal, dataFrame) });
        }

        public IReadOnlyList<ITimeSeriesFrame> Frames { get; }
    }
}
