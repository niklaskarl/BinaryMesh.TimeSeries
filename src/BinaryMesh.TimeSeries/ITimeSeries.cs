// -----------------------------------------------------------------------
// <copyright file="ITimeSeries.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A set of <see cref="ISignal"/>s belonging together, optionaly grouped by <see cref="IFrame"/>s.
    /// It also exposes metadata commonly found in time series storage formats.
    /// </summary>
    public interface ITimeSeries
    {
        /// <summary>
        /// Gets a list of all <see cref="IFrame"/>s in the time series.
        /// </summary>
        IReadOnlyList<IFrame> Frames { get; }

        /// <summary>
        /// Gets a list of all <see cref="IFrameSignal"/>s in the time series.
        /// </summary>
        IFrameSignalCollection Signals { get; }
    }
}
