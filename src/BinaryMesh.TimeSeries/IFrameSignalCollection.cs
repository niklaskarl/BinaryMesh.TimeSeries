// -----------------------------------------------------------------------
// <copyright file="IFrameSignalCollection.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A collection of <see cref="IFrameSignal"/>s which can be accessed either by index or column name.
    /// </summary>
    public interface IFrameSignalCollection : IReadOnlyList<IFrameSignal>
    {
        /// <summary>
        /// Gets the <see cref="IFrameSignal"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="IFrameSignal"/> to get.</param>
        /// <returns>The <see cref="ISignal"/> with the specified name.</returns>
        IFrameSignal this[string name] { get; }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> over the names of all <see cref="IFrameSignal"/>s in the collection.
        /// </summary>
        IEnumerable<string> Keys { get; }

        /// <summary>
        /// Determines whether the collection contains a <see cref="IFrameSignal"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="IFrameSignal"/> to locate in the collection.</param>
        /// <returns><c>true</c> if the collection contains a <see cref="IFrameSignal"/> with the specified name; otherwise, <c>false</c>.</returns>
        bool ContainsKey(string name);

        /// <summary>
        /// Gets the <see cref="ISignal"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="IFrameSignal"/> to get.</param>
        /// <param name="signal">When this method returns, contains the <see cref="IFrameSignal"/> with the specified name, if the name is found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the collection contains a <see cref="IFrameSignal"/> with the specified name; otherwise, <c>false</c>.</returns>
        bool TryGetValue(string name, out IFrameSignal signal);
    }
}
