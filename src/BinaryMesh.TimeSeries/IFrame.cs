﻿// -----------------------------------------------------------------------
// <copyright file="IFrame.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A frame of <see cref="ISignal"/>s which share the same time signal.
    /// It's signal values can be read using a <see cref="IFrameReader"/>.
    /// </summary>
    public interface IFrame
    {
        /// <summary>
        /// Gets the <see cref="ITimeSeries"/> the frame belongs to.
        /// </summary>
        ITimeSeries TimeSeries { get; }

        /// <summary>
        /// Gets the name of the frame.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a list of all <see cref="ISignal"/>s in the frame.
        /// </summary>
        IFrameSignalCollection Signals { get; }

        /// <summary>
        /// Gets the number of records in the set..
        /// </summary>
        long RecordCount { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IFrameReader"/> supports random seek operations or
        /// only forward oriented read operations.
        /// </summary>
        bool CanSeek { get; }

        /// <summary>
        /// Creates a new <see cref="IFrameReader"/> which can be used to iterate over the signal values.
        /// </summary>
        /// <returns>The newly created <see cref="IFrameReader"/>.</returns>
        IFrameReader GetReader();
    }
}
