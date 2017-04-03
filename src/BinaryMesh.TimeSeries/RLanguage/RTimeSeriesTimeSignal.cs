// -----------------------------------------------------------------------
// <copyright file="RTimeSeriesTimeSignal.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using BinaryMesh.Data.RLanguage;

namespace BinaryMesh.TimeSeries.RLanguage
{
    internal abstract class RTimeSeriesTimeSignal
    {
        public abstract DataFrameColumn Column { get; }

        public abstract TimeSpan GetOffset(long index);
    }
}
