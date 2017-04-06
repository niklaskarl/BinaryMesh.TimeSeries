// -----------------------------------------------------------------------
// <copyright file="SignalType.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// Specifies the type of <see cref="ISignal"/> values.
    /// </summary>
    public enum SignalType
    {
        /// <summary>
        /// The type of the signal values is unknown and cannot be read.
        /// </summary>
        Unknown,

        /// <summary>
        /// The signal values are real numbers and represented as IEEE-754 double-precission floating-point <see cref="double"/> values.
        /// </summary>
        Real,

        /// <summary>
        /// The signal values are strings and represented as <see cref="string"/> objects.
        /// </summary>
        String
    }
}
