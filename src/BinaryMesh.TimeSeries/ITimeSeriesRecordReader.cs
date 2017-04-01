﻿// -----------------------------------------------------------------------
// <copyright file="ITimeSeriesRecordReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// Determines the actual record when doing inaccurate, time-based seek operatrions.
    /// </summary>
    public enum SeekMode
    {
        /// <summary>
        /// Seeks to the greatest record less than or equal to the specified time.
        /// </summary>
        Floor,

        /// <summary>
        /// Seeks to the least record greater than or equal to the specified time.
        /// </summary>
        Ceiling
    }

    /// <summary>
    /// Supports forward reading and, if supported, random seeking of records in a <see cref="ITimeSeriesFrame"/>.
    /// </summary>
    public interface ITimeSeriesRecordReader
    {
        /// <summary>
        /// Gets the index of the current record.
        /// </summary>
        long CurrentIndex { get; }

        /// <summary>
        /// Gets the current time offset relative to the frame's start of the measurement.
        /// </summary>
        TimeSpan CurrentOffset { get; }

        /// <summary>
        /// Advances the reader to the next record.
        /// </summary>
        /// <returns><c>true</c> if the reader was successfully advanced to the next record; <c>false</c> if the reader has passed the end of the frame.</returns>
        bool Read();

        /// <summary>
        /// Moves the reader to the record with the specified index.
        /// </summary>
        /// <param name="index">The index to seek for.</param>
        void Seek(long index);

        /// <summary>
        /// Moves the reader to the record with the specified time offset.
        /// </summary>
        /// <param name="offset">The time offset to seek for, relative to the frame's start of the measurement.</param>
        /// <param name="mode">The mode used to determine the actual record.</param>
        void Seek(TimeSpan offset, SeekMode mode);
    }
}