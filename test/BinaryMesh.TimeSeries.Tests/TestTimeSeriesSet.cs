// -----------------------------------------------------------------------
// <copyright file="TestTimeSeriesSet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryMesh.TimeSeries.Tests
{
    [TestClass]
    public class TestTimeSeriesSet
    {
        [TestMethod]
        public void TestIntegration()
        {
            using (Stream stream = new FileStream("C:\\Users\\nikla\\Downloads\\2016-05-13_213_690_FREE_RUHSTU_02_auto4_F07.dat", FileMode.Open, FileAccess.Read))
            {
                ITimeSeries timeSeries = MdfTimeSeriesBuilder.BuildMdfTimeSeriesSetFromStream(stream);

                timeSeries.Frames[1].Signals["test"].GetReader(TimeSpan.Zero);
            }
        }
    }
}
