// -----------------------------------------------------------------------
// <copyright file="MdfTimeSeries.cs" company="Binary Mesh">
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
    internal class MdfTimeSeries : ITimeSeries
    {
        private MdfFile _file;

        internal MdfTimeSeries(MdfFile file)
        {
            _file = file;
            Frames = new FrameCollection(_file.ChannelGroups.Select(g => new MdfFrame(this, g)).ToArray());
            Signals = new FrameSignalCollection(Frames.SelectMany(f => f.Signals).ToArray());
        }

        public IReadOnlyList<IFrame> Frames { get; }

        public IFrameSignalCollection Signals { get; }
    }
}
