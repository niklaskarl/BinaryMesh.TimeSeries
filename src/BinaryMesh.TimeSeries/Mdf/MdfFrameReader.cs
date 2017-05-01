// -----------------------------------------------------------------------
// <copyright file="MdfFrameReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using BinaryMesh.Data.Mdf;

namespace BinaryMesh.TimeSeries.Mdf
{
    internal sealed class MdfFrameReader : IDiscreteFrameReader
    {
        private readonly MdfFrame _frame;

        private readonly MdfRecordReader _reader;

        private readonly MdfChannel _timeChannel;

        private long _index;

        internal MdfFrameReader(MdfFrame frame)
        {
            _frame = frame;
            _reader = _frame.ChannelGroup.GetRecordReader();
            _timeChannel = _frame.ChannelGroup.TimeChannel;

            _index = -1;
        }

        public IFrame Frame => _frame;

        public long CurrentIndex
        {
            get
            {
                if (_index < 0)
                {
                    throw new InvalidOperationException();
                }

                return _index;
            }
        }

        public double CurrentOffset
        {
            get
            {
                if (_index < 0)
                {
                    throw new InvalidOperationException();
                }

                return _reader.GetReal(_timeChannel);
            }
        }

        public bool Read()
        {
            if (_reader.Read())
            {
                _index++;
                return true;
            }

            return false;
        }

        public void Seek(long index)
        {
            throw new NotSupportedException();
        }

        public void Seek(TimeSpan offset, SeekMode mode)
        {
            throw new NotSupportedException();
        }

        public bool IsNull(int signalIndex)
        {
            return IsNull(_frame.Signals[signalIndex]);
        }

        public bool IsNull(ISignal signal)
        {
            MdfChannel channel = ((MdfSignal)signal).Channel;
            return _reader.IsNull(channel);
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
            return TryGetReal(_frame.Signals[signalIndex], out value);
        }

        public bool TryGetReal(ISignal signal, out double value)
        {
            MdfChannel channel = ((MdfSignal)signal).Channel;
            return _reader.TryGetReal(channel, out value);
        }

        public string GetString(int signalIndex)
        {
            return GetString(_frame.Signals[signalIndex]);
        }

        public string GetString(ISignal signal)
        {
            MdfChannel channel = ((MdfSignal)signal).Channel;
            return _reader.GetText(channel);
        }
    }
}
