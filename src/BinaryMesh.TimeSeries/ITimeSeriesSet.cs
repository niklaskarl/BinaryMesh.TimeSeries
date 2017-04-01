// -----------------------------------------------------------------------
// <copyright file="ITimeSeriesSet.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A set of <see cref="ITimeSeriesSignal"/>s belonging together, grouped by <see cref="ITimeSeriesFrame"/>s.
    /// It also exposes metadata commonly found in time series storage formats.
    /// </summary>
    public interface ITimeSeriesSet
    {
        /// <summary>
        /// Gets a list of all <see cref="ITimeSeriesFrame"/>s in the set.
        /// </summary>
        IReadOnlyList<ITimeSeriesFrame> Frames { get; }
    }
}
