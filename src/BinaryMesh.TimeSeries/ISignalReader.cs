// -----------------------------------------------------------------------
// <copyright file="ISignalReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A continuous reader that iterates over the values of a <see cref="ISignal"/>.
    /// </summary>
    public interface ISignalReader
    {
        /// <summary>
        /// Gets the <see cref="ISignal"/> the reader iterates over.
        /// </summary>
        ISignal Signal { get; }

        /// <summary>
        /// Gets the time the reader currently points at, relative to the start time of the measurement.
        /// </summary>
        TimeSpan CurrentTime { get; }

        /// <summary>
        /// Advances the reader forward by the specified offset.
        /// </summary>
        /// <param name="offset">The offset by which to advance the reader.</param>
        /// <returns><c>true</c> if the reader has been moved outside the <see cref="ISignal"/>'s range;<c>false</c> otherwise.</returns>
        bool MoveForward(TimeSpan offset);

        /// <summary>
        /// Checks whether the <see cref="ISignal"/> value at the current position of the reader is <c>null</c>.
        /// </summary>
        /// <returns><c>true</c> if the <see cref="ISignal"/> value is <c>null</c>; <c>false</c> otherwise.</returns>
        bool IsNull();

        /// <summary>
        /// Gets the <see cref="ISignal"/> value at the current position of the reader as a real number.
        /// </summary>
        /// <returns>The <see cref="ISignal"/> value at the current position of the reader.</returns>
        double GetReal();

        /// <summary>
        /// Gets the <see cref="ISignal"/> value at the current position of the reader.
        /// </summary>
        /// <param name="value">When this method returns, contains the <see cref="ISignal"/> value at the current position of the reader, if it is not <c>null</c>.</param>
        /// <returns><c>true</c> if the <see cref="ISignal"/> value is not <c>null</c>; <c>false</c> otherwise.</returns>
        bool TryGetReal(out double value);

        /// <summary>
        /// Gets the <see cref="ISignal"/> value at the current position of the reader as a string.
        /// </summary>
        /// <returns>The <see cref="ISignal"/> value at the current position of the reader.</returns>
        string GetString();
    }
}
