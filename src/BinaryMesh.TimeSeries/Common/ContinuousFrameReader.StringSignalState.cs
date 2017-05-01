using System;

namespace BinaryMesh.TimeSeries.Common
{
    internal sealed partial class ContinuousFrameReader
    {
        private sealed class StringSignalState : ISignalState
        {
            private readonly int _index;

            private string _lastValue;

            private string _nextValue;

            public StringSignalState(int index)
            {
                _index = index;
            }

            public void Initialize(IDiscreteFrameReader reader, double readerTime)
            {
                _lastValue = reader.GetString(_index);
                _nextValue = _lastValue;
            }

            public void Advance(IDiscreteFrameReader reader, double readerTime)
            {
                _lastValue = _nextValue;
                _nextValue = reader.GetString(_index);
            }

            public bool IsNull(double currentTime)
            {
                return _lastValue == null;
            }

            public bool TryGetReal(double currentTime, out double value)
            {
                throw new InvalidOperationException();
            }

            public string GetString(double currentTime)
            {
                return _lastValue;
            }
        }
    }
}
