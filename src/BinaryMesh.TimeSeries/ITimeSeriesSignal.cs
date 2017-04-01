// -----------------------------------------------------------------------
// <copyright file="ITimeSeriesSignal.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A signal 
    /// </summary>
    public interface ITimeSeriesSignal
    {
        /// <summary>
        /// Gets the frame the signal is contained in.
        /// </summary>
        ITimeSeriesFrame Frame { get; }

        /// <summary>
        /// 
        /// </summary>
        TimeSeriesSignalType SignalType { get; }

        /// <summary>
        /// Gets the unique name of the signal.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the human friendly name of the signal.
        /// </summary>
        string DisplayName { get; }
    }
}
