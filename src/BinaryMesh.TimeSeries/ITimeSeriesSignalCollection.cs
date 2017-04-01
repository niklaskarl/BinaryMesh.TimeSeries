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
    public interface ITimeSeriesSignalCollection : IReadOnlyList<ITimeSeriesSignal>, IReadOnlyDictionary<string, ITimeSeriesSignal>
    {
    }
}
