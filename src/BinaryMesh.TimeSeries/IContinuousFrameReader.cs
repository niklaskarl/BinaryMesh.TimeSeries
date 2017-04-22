// -----------------------------------------------------------------------
// <copyright file="IContinuousFrameReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// Supports forward reading and, if supported, random seeking of continuous signal values in a <see cref="IFrame"/>.
    /// </summary>
    public interface IContinuousFrameReader
    {
        /// <summary>
        /// Gets the <see cref="IFrame"/> the reader iterates over.
        /// </summary>
        IFrame Frame { get; }

        /// <summary>
        /// Gets the time the reader currently points at, relative to the start time of the <see cref="IFrame"/>, in seconds.
        /// </summary>
        double CurrentTime { get; }

        /// <summary>
        /// Advances the reader forward by the specified offset.
        /// </summary>
        /// <param name="offset">The offset by which to advance the reader, in seconds.</param>
        /// <returns><c>true</c> if the reader has been moved outside the <see cref="ISignal"/>'s range;<c>false</c> otherwise.</returns>
        bool MoveForward(double offset);

        /// <summary>
        /// Checks whether the <see cref="ISignal"/> value at the current position of the reader is <c>null</c>.
        /// </summary>
        /// <param name="signalIndex">The index of the signal whose value to check.</param>
        /// <returns><c>true</c> if the <see cref="ISignal"/> value is <c>null</c>; <c>false</c> otherwise.</returns>
        bool IsNull(int signalIndex);

        /// <summary>
        /// Gets the <see cref="ISignal"/> value at the current position of the reader as a real number.
        /// </summary>
        /// <param name="signalIndex">The index of the signal for which to get the value.</param>
        /// <returns>The <see cref="ISignal"/> value at the current position of the reader.</returns>
        double GetReal(int signalIndex);

        /// <summary>
        /// Gets the <see cref="ISignal"/> value at the current position of the reader.
        /// </summary>
        /// <param name="signalIndex">The index of the signal for which to get the value.</param>
        /// <param name="value">When this method returns, contains the <see cref="ISignal"/> value at the current position of the reader, if it is not <c>null</c>.</param>
        /// <returns><c>true</c> if the <see cref="ISignal"/> value is not <c>null</c>; <c>false</c> otherwise.</returns>
        bool TryGetReal(int signalIndex, out double value);

        /// <summary>
        /// Gets the <see cref="ISignal"/> value at the current position of the reader as a string.
        /// </summary>
        /// <param name="signalIndex">The index of the signal.</param>
        /// <returns>The <see cref="ISignal"/> value at the current position of the reader.</returns>
        string GetString(int signalIndex);
    }
}
