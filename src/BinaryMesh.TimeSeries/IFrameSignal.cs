// -----------------------------------------------------------------------
// <copyright file="IFrameSignal.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A <see cref="ISignal"/> that is part of a <see cref="IFrame"/>
    /// </summary>
    public interface IFrameSignal : ISignal
    {
        /// <summary>
        /// Gets the <see cref="IFrame"/> the signal is contained in.
        /// </summary>
        IFrame Frame { get; }

        /// <summary>
        /// Gets the index of the signal in the <see cref="IFrame"/>.
        /// </summary>
        int Index { get; }

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
