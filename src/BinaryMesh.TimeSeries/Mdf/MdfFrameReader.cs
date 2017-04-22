﻿// -----------------------------------------------------------------------
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

                return (double)_reader.GetValue(_timeChannel);
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
            /*
             * The MDF format doesn't support 'null' values.
             * So simply return false.
             */
            return false;
        }

        public double GetReal(int signalIndex)
        {
            return GetReal(_frame.Signals[signalIndex]);
        }

        public double GetReal(ISignal signal)
        {
            MdfChannel channel = ((MdfSignal)signal).Channel;
            switch (channel.DataType)
            {
                case MdfDataType.FloatingPoint:
                    return (double)_reader.GetValue(channel);
                case MdfDataType.Integer:
                    return (long)_reader.GetValue(channel);
                case MdfDataType.UnsignedInteger:
                    return (ulong)_reader.GetValue(channel);
                default:
                    throw new InvalidCastException();
            }
        }

        public bool TryGetReal(int signalIndex, out double value)
        {
            /*
             * The MDF format doesn't support 'null' values.
             * So simply get the value and return true.
             */

            value = GetReal(signalIndex);
            return true;
        }

        public string GetString(int signalIndex)
        {
            return GetString(_frame.Signals[signalIndex]);
        }

        public string GetString(ISignal signal)
        {
            MdfChannel channel = ((MdfSignal)signal).Channel;
            switch (channel.DataType)
            {
                case MdfDataType.String:
                    return (string)_reader.GetValue(channel);
                default:
                    throw new InvalidCastException();
            }
        }
    }
}
