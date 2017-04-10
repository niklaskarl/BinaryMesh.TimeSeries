// -----------------------------------------------------------------------
// <copyright file="ISignal.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A series of values defined over a period of time.
    /// </summary>
    public interface ISignal
    {
        /// <summary>
        /// Gets the data type of the signal.
        /// </summary>
        SignalType SignalType { get; }

        /// <summary>
        /// Gets the relative start time of the signal.
        /// </summary>
        TimeSpan StartTime { get; }

        /// <summary>
        /// Gets the duration for which the signal is defined.
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Gets a value indicating whether absolute time values are defined for the signal.
        /// </summary>
        bool HasAbsoluteTime { get; }

        /// <summary>
        /// Gets the absolute start time of the signal, if defined.
        /// </summary>
        DateTime AbsoluteStartTime { get; }

        /// <summary>
        /// Creates a new <see cref="ISignalReader"/> for the signal.
        /// </summary>
        /// <returns>The created <see cref="ISignalReader"/> for the signal.</returns>
        ISignalReader GetReader(TimeSpan startTime);
    }
}
