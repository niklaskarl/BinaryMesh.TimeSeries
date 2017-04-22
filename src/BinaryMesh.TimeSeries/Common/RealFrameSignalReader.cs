// -----------------------------------------------------------------------
// <copyright file="RealFrameSignalReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries.Common
{
    internal sealed class RealFrameSignalReader : ISignalReader
    {
        private readonly IFrameSignal _signal;

        private readonly IDiscreteFrameReader _reader;

        private double _currentTime;

        private double _lastTime;

        private double _nextTime;

        private double _lastValue;

        private double _nextValue;

        private bool _isLastNull;

        private bool _isNextNull;

        private bool _endOfSignal;

        public RealFrameSignalReader(IFrameSignal signal, double startTime)
        {
            _signal = signal;
            _currentTime = startTime;
            _reader = _signal.Frame.GetDiscreteReader();

            Initialize();
        }

        public ISignal Signal => _signal;

        public double CurrentTime => _currentTime;

        public bool MoveForward(double offset)
        {
            if (offset < 0.0)
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

        public double GetReal()
        {
            if (TryGetReal(out double value))
            {
                return value;
            }

            throw new InvalidOperationException();
        }

        public bool TryGetReal(out double value)
        {
            if (IsNull())
            {
                value = default(double);
                return false;
            }

            if (_currentTime == _lastTime)
            {
                value = _lastValue;
            }
            else
            {
                value = _lastValue + (((_currentTime - _lastTime) / (_nextTime - _lastTime)) * (_nextValue - _lastValue));
            }

            return true;
        }

        public string GetString()
        {
            throw new InvalidOperationException();
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
                while (_nextTime < _currentTime && _reader.Read())
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
