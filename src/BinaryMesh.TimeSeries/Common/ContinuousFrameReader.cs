// -----------------------------------------------------------------------
// <copyright file="ContinuousFrameReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries.Common
{
    internal sealed partial class ContinuousFrameReader : IContinuousFrameReader
    {
        private readonly IFrame _frame;

        private readonly IDiscreteFrameReader _reader;

        private readonly ISignalState[] _signalStates;

        private double _currentTime;

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
            if (_endOfSignal)
            {
                return true;
            }

            return _signalStates[signalIndex]?.IsNull(_currentTime) ?? throw new InvalidOperationException();
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
            if (_endOfSignal)
            {
                return false;
            }

            return _signalStates[signalIndex]?.TryGetReal(_currentTime, out value) ?? throw new InvalidOperationException();
        }

        public string GetString(int signalIndex)
        {
            return _signalStates[signalIndex]?.GetString(_currentTime) ?? throw new InvalidOperationException();
        }

        private void Initialize()
        {
            if (_reader.Read())
            {
                double time = _reader.CurrentOffset;
                foreach (ISignalState state in _signalStates)
                {
                    state?.Initialize(_reader, time);
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
                double time = _reader.CurrentOffset;
                while (time < _currentTime && _reader.Read())
                {
                    time = _reader.CurrentOffset;

                    foreach (ISignalState state in _signalStates)
                    {
                        state?.Advance(_reader, time);
                    }
                }

                if (time < _currentTime)
                {
                    _endOfSignal = true;
                }
            }
        }
    }
}
