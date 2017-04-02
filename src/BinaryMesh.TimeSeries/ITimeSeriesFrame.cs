// -----------------------------------------------------------------------
// <copyright file="ITimeSeriesFrame.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A frame of <see cref="ITimeSeriesSignal"/>s which share the same time signal.
    /// It's signal values can be read using a <see cref="ITimeSeriesRecordReader"/>.
    /// </summary>
    public interface ITimeSeriesFrame
    {
        /// <summary>
        /// Gets the <see cref="ITimeSeriesSet"/> the frame belongs to.
        /// </summary>
        ITimeSeriesSet Set { get; }

        /// <summary>
        /// Gets the name of the frame.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a list of all <see cref="ITimeSeriesSignal"/>s in the frame.
        /// </summary>
        ITimeSeriesSignalCollection Signals { get; }

        /// <summary>
        /// Gets the number of records in the set..
        /// </summary>
        long RecordCount { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ITimeSeriesRecordReader"/> supports random seek operations or
        /// only forward oriented read operations.
        /// </summary>
        bool CanSeek { get; }

        /// <summary>
        /// Creates a new <see cref="ITimeSeriesRecordReader"/> which can be used to iterate over the signal values.
        /// </summary>
        /// <returns>The newly created <see cref="ITimeSeriesRecordReader"/>.</returns>
        ITimeSeriesRecordReader GetRecordReader();
    }
}
