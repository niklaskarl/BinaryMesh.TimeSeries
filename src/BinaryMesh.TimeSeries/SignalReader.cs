using System;

namespace BinaryMesh.TimeSeries
{
    internal sealed class SignalReader : ISignalReader
    {
        private readonly IFrameSignal _signal;

        private readonly IFrameReader _reader;

        private TimeSpan _currentTime;

        private TimeSpan _lastTime;

        private TimeSpan _nextTime;

        private double _lastValue;

        private double _nextValue;

        private bool _isLastNull;

        private bool _isNextNull;

        private bool _endOfSignal;

        public SignalReader(IFrameSignal signal)
        {
            _signal = signal;
            // TODO _signalIndex
            _reader = _signal.Frame.GetDiscreteReader();
        }

        public ISignal Signal => _signal;

        public TimeSpan CurrentTime => _currentTime;

        public bool MoveForward(TimeSpan offset)
        {
            if (offset < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }

            _currentTime += offset;

            SeekToCurrentTime();

            return !_endOfSignal;
        }

        public bool IsNull()
        {
            if (_endOfSignal)
            {
                return true;
            }

            if (_currentTime < _lastTime)
            {
                return true;
            }

            return _isLastNull || _isNextNull;
        }

        public bool TryGetReal(out double value)
        {
            if (IsNull())
            {
                value = default(double);
                return false;
            }

            long ticks = _nextTime.Ticks - _lastTime.Ticks;
            long currentTicks = _currentTime.Ticks - _lastTime.Ticks;

            if (currentTicks == 0)
            {
                value = _lastValue;
            }
            else
            {
                value = _lastValue + ((double)currentTicks / ticks) * (_nextValue - _lastValue);
            }

            return true;
        }

        public double GetReal()
        {
            if (TryGetReal(out double value))
            {
                return value;
            }

            throw new InvalidOperationException();
        }

        public string GetString()
        {
            throw new NotSupportedException();
        }

        private void Initialize()
        {
            if (_reader.Read())
            {
                _lastTime = _reader.CurrentOffset;
                _isLastNull = _reader.TryGetReal(_signal.Index, out _lastValue);

                _nextTime = _lastTime;
                _nextValue = _lastValue;
                _isNextNull = _isLastNull;

                SeekToCurrentTime();
            }
            else
            {
                _endOfSignal = true;
            }
        }

        private void SeekToCurrentTime()
        {
            if (!_endOfSignal)
            {
                while (_reader.Read() && _nextTime < _currentTime)
                {
                    _lastTime = _nextTime;
                    _lastValue = _nextValue;
                    _isLastNull = _isNextNull;

                    _nextTime = _reader.CurrentOffset;
                    _isNextNull = _reader.TryGetReal(_signal.Index, out _nextValue);
                }

                if (_nextTime < _currentTime)
                {
                    _endOfSignal = true;
                }
            }
        }
    }
}
