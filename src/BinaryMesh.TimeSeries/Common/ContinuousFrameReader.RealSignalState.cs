using System;

namespace BinaryMesh.TimeSeries.Common
{
    internal sealed partial class ContinuousFrameReader
    {
        private sealed class RealSignalState : ISignalState
        {
            private readonly int _index;

            private double _lastTime;

            private double _nextTime;

            private double _lastValue;

            private double _nextValue;

            private bool _isLastNull;

            private bool _isNextNull;

            public RealSignalState(int index)
            {
                _index = index;
            }

            public void Initialize(IDiscreteFrameReader reader, double readerTime)
            {
                _lastTime = readerTime;
                _nextTime = _lastTime;

                _isLastNull = !reader.TryGetReal(_index, out _lastValue);

                _nextValue = _lastValue;
                _isNextNull = _isLastNull;
            }

            public void Advance(IDiscreteFrameReader reader, double readerTime)
            {
                _lastTime = _nextTime;
                _nextTime = readerTime;

                _lastValue = _nextValue;
                _isLastNull = _isNextNull;
                _isNextNull = !reader.TryGetReal(_index, out _nextValue);
            }

            public bool IsNull(double currentTime)
            {
                return _isLastNull || _isNextNull || currentTime < _lastTime;
            }

            public bool TryGetReal(double currentTime, out double value)
            {
                if (IsNull(currentTime))
                {
                    value = default(double);
                    return false;
                }

                if (currentTime == _lastTime)
                {
                    value = _lastValue;
                }
                else
                {
                    value = _lastValue + (((currentTime - _lastTime) / (_nextTime - _lastTime)) * (_nextValue - _lastValue));
                }

                return true;
            }

            public string GetString(double currentTime)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
