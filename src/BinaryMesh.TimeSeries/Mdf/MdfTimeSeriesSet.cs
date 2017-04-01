// -----------------------------------------------------------------------
// <copyright file="MdfTimeSeriesSet.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BinaryMesh.Data.Mdf;

namespace BinaryMesh.TimeSeries.Mdf
{
    internal class MdfTimeSeriesSet : ITimeSeriesSet
    {
        private MdfFile _file;

        internal MdfTimeSeriesSet(MdfFile file)
        {
            _file = file;
            Frames = new TimeSeriesFrameCollection(_file.ChannelGroups.Select(g => new MdfTimeSeriesFrame(this, g)).ToArray());
        }

        public IReadOnlyList<ITimeSeriesFrame> Frames { get; }
    }
}
