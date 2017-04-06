// -----------------------------------------------------------------------
// <copyright file="ISignal.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A signal in a <see cref="IFrame"/>.
    /// </summary>
    public interface ISignal
    {
        /// <summary>
        /// Gets the data type of the signal.
        /// </summary>
        SignalType SignalType { get; }

        /// <summary>
        /// Gets the unique name of the signal.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the human friendly name of the signal.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the time of the first measurement data point in the signal.
        /// </summary>
        DateTime StartTime { get; }

        TimeSpan Duration { get; }


    }
}
