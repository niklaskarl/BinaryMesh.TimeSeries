using System;
using System.IO;
using BinaryMesh.Data.Mdf;
using BinaryMesh.TimeSeries.Mdf;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// Exposes methods for creating <see cref="ITimeSeriesSet"/>s from ASAM MDF files.
    /// </summary>
    public static class MdfTimeSeriesBuilder
    {
        /// <summary>
        /// Creates a new <see cref="ITimeSeriesSet"/> from a stream pointing to a ASAM MDF file.
        /// The stream will be captured by the <see cref="ITimeSeriesSet"/> for it's whole lifetime and must not be disposed before the <see cref="ITimeSeriesSet"/> is disposed.
        /// </summary>
        /// <param name="stream">The stream from which to read the file.</param>
        /// <returns>The newly created <see cref="ITimeSeriesSet"/>.</returns>
        public static ITimeSeriesSet BuildMdfTimeSeriesSetFromStream(Stream stream)
        {
            MdfFile file = new MdfFile(stream);
            return new MdfTimeSeriesSet(file);
        }
    }
}
