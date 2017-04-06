// -----------------------------------------------------------------------
// <copyright file="MdfTimeSeriesBuilder.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using BinaryMesh.Data.Mdf;
using BinaryMesh.TimeSeries.Mdf;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// Exposes methods for creating <see cref="ITimeSeries"/>s from ASAM MDF files.
    /// </summary>
    public static class MdfTimeSeriesBuilder
    {
        /// <summary>
        /// Creates a new <see cref="ITimeSeries"/> from a stream pointing to a ASAM MDF file.
        /// The stream will be captured by the <see cref="ITimeSeries"/> for it's whole lifetime and must not be disposed before the <see cref="ITimeSeries"/> is disposed.
        /// </summary>
        /// <param name="stream">The stream from which to read the file.</param>
        /// <returns>The newly created <see cref="ITimeSeries"/>.</returns>
        public static ITimeSeries BuildMdfTimeSeriesSetFromStream(Stream stream)
        {
            MdfFile file = new MdfFile(stream);
            return new MdfTimeSeries(file);
        }
    }
}
