﻿// -----------------------------------------------------------------------
// <copyright file="ContinuousFrameReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries.Common
{
    internal sealed class ContinuousFrameReader : IContinuousFrameReader
    {
        private readonly IFrame _frame;

        private readonly IDiscreteFrameReader _reader;

        private readonly ISignalState[] _signalStates;

        private double _currentTime;

        private double _lastTime;

        private double _nextTime;

        private bool _endOfSignal;

        public ContinuousFrameReader(IFrame frame, double startTime)
        {
            _frame = frame;
            _currentTime = startTime;
            _reader = _frame.GetDiscreteReader();

            _signalStates = new ISignalState[_frame.Signals.Count];
            for (int i = 0; i < _frame.Signals.Count; i++)
            {
                IFrameSignal signal = _frame.Signals[i];
                switch (signal.SignalType)
                {
                    case SignalType.Real:
                        _signalStates[i] = new RealSignalState(i);
                        break;
                    case SignalType.String:
                        _signalStates[i] = new StringSignalState(i);
                        break;
                    case SignalType.Unknown:
                        _signalStates[i] = null;
                        break;
                    default:
                        throw new Exception();
                }
            }

            Initialize();
        }

        private interface ISignalState
        {
            void Initialize(IDiscreteFrameReader reader);

            void Advance(IDiscreteFrameReader reader);

            bool IsNull();

            bool TryGetReal(double currentTime, double lastTime, double nextTime, out double value);

            string GetString();
        }

        public IFrame Frame => _frame;

        public double CurrentTime => throw new NotImplementedException();

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

        public bool IsNull(int signalIndex)
        {
            if (_endOfSignal || _currentTime < _lastTime)
            {
                return true;
            }

            return _signalStates[signalIndex]?.IsNull() ?? throw new InvalidOperationException();
        }

        public double GetReal(int signalIndex)
        {
            if (TryGetReal(signalIndex, out double value))
            {
                return value;
            }

            throw new InvalidOperationException();
        }

        public bool TryGetReal(int signalIndex, out double value)
        {
            value = default(double);
            if (_endOfSignal || _currentTime < _lastTime)
            {
                return false;
            }

            return _signalStates[signalIndex]?.TryGetReal(_currentTime, _lastTime, _nextTime, out value) ?? throw new InvalidOperationException();
        }

        public string GetString(int signalIndex)
        {
            return _signalStates[signalIndex]?.GetString() ?? throw new InvalidOperationException();
        }

        private void Initialize()
        {
            if (_reader.Read())
            {
                _lastTime = _reader.CurrentOffset;
                _nextTime = _lastTime;

                foreach (ISignalState state in _signalStates)
                {
                    state?.Initialize(_reader);
                }

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
                    _nextTime = _reader.CurrentOffset;

                    foreach (ISignalState state in _signalStates)
                    {
                        state?.Advance(_reader);
                    }
                }

                if (_nextTime < _currentTime)
                {
                    _endOfSignal = true;
                }
            }
        }

        private sealed class RealSignalState : ISignalState
        {
            private readonly int _index;

            private double _lastValue;

            private double _nextValue;

            private bool _isLastNull;

            private bool _isNextNull;

            public RealSignalState(int index)
            {
                _index = index;
            }

            public void Initialize(IDiscreteFrameReader reader)
            {
                _isLastNull = reader.TryGetReal(_index, out _lastValue);

                _nextValue = _lastValue;
                _isNextNull = _isLastNull;
            }

            public void Advance(IDiscreteFrameReader reader)
            {
                _lastValue = _nextValue;
                _isLastNull = _isNextNull;
                _isNextNull = reader.TryGetReal(_index, out _nextValue);
            }

            public bool IsNull()
            {
                return _isLastNull || _isNextNull;
            }

            public bool TryGetReal(double currentTime, double lastTime, double nextTime, out double value)
            {
                if (IsNull())
                {
                    value = default(double);
                    return false;
                }

                if (currentTime == lastTime)
                {
                    value = _lastValue;
                }
                else
                {
                    value = _lastValue + (((currentTime - lastTime) / (nextTime - lastTime)) * (_nextValue - _lastValue));
                }

                return true;
            }

            public string GetString()
            {
                throw new InvalidOperationException();
            }
        }

        private sealed class StringSignalState : ISignalState
        {
            private readonly int _index;

            private string _lastValue;

            private string _nextValue;

            public StringSignalState(int index)
            {
                _index = index;
            }

            public void Initialize(IDiscreteFrameReader reader)
            {
                _lastValue = reader.GetString(_index);
                _nextValue = _lastValue;
            }

            public void Advance(IDiscreteFrameReader reader)
            {
                _lastValue = _nextValue;
                _nextValue = reader.GetString(_index);
            }

            public bool IsNull()
            {
                return _lastValue == null;
            }

            public bool TryGetReal(double currentTime, double lastTime, double nextTime, out double value)
            {
                throw new InvalidOperationException();
            }

            public string GetString()
            {
                return _lastValue;
            }
        }
    }
}
