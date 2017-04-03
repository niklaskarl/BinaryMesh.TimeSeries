// -----------------------------------------------------------------------
// <copyright file="ITimeSeriesSignalCollection.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// A collection of <see cref="ITimeSeriesSignal"/>s which can be accessed either by index or column name.
    /// </summary>
    public interface ITimeSeriesSignalCollection : IReadOnlyList<ITimeSeriesSignal>
    {
        /// <summary>
        /// Gets the <see cref="ITimeSeriesSignal"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="ITimeSeriesSignal"/> to get.</param>
        /// <returns>The <see cref="ITimeSeriesSignal"/> with the specified name.</returns>
        ITimeSeriesSignal this[string name] { get; }
        
        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> over the names of all <see cref="ITimeSeriesSignal"/>s in the collection.
        /// </summary>
        IEnumerable<string> Keys { get; }

        /// <summary>
        /// Determines whether the collection contains a <see cref="ITimeSeriesSignal"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="ITimeSeriesSignal"/> to locate in the collection.</param>
        /// <returns><c>true</c> if the collection contains a <see cref="ITimeSeriesSignal"/> with the specified name; otherwise, <c>false</c>.</returns>
        bool ContainsKey(string name);

        /// <summary>
        /// Gets the <see cref="ITimeSeriesSignal"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="ITimeSeriesSignal"/> to get.</param>
        /// <param name="signal">When this method returns, contains the <see cref="ITimeSeriesSignal"/> with the specified name, if the name is found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the collection contains a <see cref="ITimeSeriesSignal"/> with the specified name; otherwise, <c>false</c>.</returns>
        bool TryGetValue(string name, out ITimeSeriesSignal signal);
    }
}
