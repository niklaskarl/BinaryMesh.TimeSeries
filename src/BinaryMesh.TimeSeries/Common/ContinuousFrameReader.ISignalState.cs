using System;

namespace BinaryMesh.TimeSeries.Common
{
    internal sealed partial class ContinuousFrameReader
    {
        private interface ISignalState
        {
            void Initialize(IDiscreteFrameReader reader, double readerTime);

            void Advance(IDiscreteFrameReader reader, double readerTime);

            bool IsNull(double currentTime);

            bool TryGetReal(double currentTime, out double value);

            string GetString(double currentTime);
        }
    }
}
