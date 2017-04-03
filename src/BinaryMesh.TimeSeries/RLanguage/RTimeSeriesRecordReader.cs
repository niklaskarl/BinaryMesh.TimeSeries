// -----------------------------------------------------------------------
// <copyright file="RTimeSeriesRecordReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.TimeSeries.RLanguage
{
    internal sealed class RTimeSeriesRecordReader : ITimeSeriesRecordReader
    {
        private readonly RTimeSeriesFrame _frame;

        private long _index;

        internal RTimeSeriesRecordReader(RTimeSeriesFrame frame)
        {
            _frame = frame;

            _index = -1;
        }

        public long CurrentIndex
        {
            get
            {
                if (_index < 0 || _index >= _frame.RecordCount)
                {
                    throw new InvalidOperationException();
                }

                return _index;
            }
        }

        public TimeSpan CurrentOffset => _frame.TimeSignal.GetOffset(_index);

        public bool IsNull(int signalIndex)
        {
            return (_frame.Signals[signalIndex] as RTimeSeriesSignal).Column.Vector.IsNull(_index);
        }

        public double GetReal(int signalIndex)
        {
            return (_frame.Signals[signalIndex] as RTimeSeriesSignal).Column.Vector.GetReal(_index);
        }

        public bool TryGetReal(int signalIndex, out double value)
        {
            return (_frame.Signals[signalIndex] as RTimeSeriesSignal).Column.Vector.TryGetReal(_index, out value);
        }

        public string GetString(int signalIndex)
        {
            return (_frame.Signals[signalIndex] as RTimeSeriesSignal).Column.Vector.GetString(_index);
        }

        public bool Read()
        {
            if (_index == _frame.RecordCount - 1)
            {
                _index = _frame.RecordCount;
                return false;
            }

            _index++;
            return true;
        }

        public void Seek(long index)
        {
            if (index < 0 || index >= _frame.RecordCount)
            {
                throw new IndexOutOfRangeException();
            }

            _index = index;
        }

        public void Seek(TimeSpan offset, SeekMode mode)
        {
            /*
             * Initialize the bounds for the binary search to the first and last record.
             */
            long lowerBound = 0;
            TimeSpan lowerOffset = _frame.TimeSignal.GetOffset(lowerBound);

            long upperBound = _frame.RecordCount - 1;
            TimeSpan upperOffset = _frame.TimeSignal.GetOffset(upperBound);

            if (offset <= lowerOffset)
            {
                _index = lowerBound;
                return;
            }
            else if (offset >= upperOffset)
            {
                _index = upperBound;
                return;
            }

            /*
             * Narrow the bounds using some sort of sophisticated binary search.
             * Assume that all offsets are equidistant and calculate a narrow index using
             * the distance between the first and last time-offset.
             */
            long bound = 0;
            TimeSpan boundOffset = lowerOffset;
            while (lowerBound < upperBound)
            {
                double rel = (offset - lowerOffset).Ticks / (double)(upperOffset - lowerOffset).Ticks;
                bound = lowerBound + (long)(rel * (upperBound - lowerBound));
                boundOffset = _frame.TimeSignal.GetOffset(bound);

                if (bound == lowerBound || bound == upperBound)
                {
                    break;
                }

                if (offset < boundOffset)
                {
                    upperBound = bound;
                    upperOffset = boundOffset;
                }
                else if (offset > boundOffset)
                {
                    lowerBound = bound;
                    lowerOffset = boundOffset;
                }
                else
                {
                    _index = bound;
                    return;
                }
            }

            /*
             * The record cannot be narrowed any better. Now seek the actual record sequentaly.
             */
            if (offset < boundOffset)
            {
                // seek upwards
                while (bound < _frame.RecordCount)
                {
                    if (offset == boundOffset)
                    {
                        _index = bound;
                        return;
                    }

                    if (offset > boundOffset)
                    {
                        switch (mode)
                        {
                            case SeekMode.Ceiling:
                                _index = bound;
                                return;
                            case SeekMode.Floor:
                                _index = Math.Max(bound - 1, 0);
                                return;
                        }
                    }

                    bound++;
                    boundOffset = _frame.TimeSignal.GetOffset(bound);
                }

                _index = _frame.RecordCount;
                return;
            }
            else if (offset > boundOffset)
            {
                // seek downwards
                while (bound >= 0)
                {
                    if (offset == boundOffset)
                    {
                        _index = bound;
                        return;
                    }

                    if (offset < boundOffset)
                    {
                        switch (mode)
                        {
                            case SeekMode.Ceiling:
                                _index = Math.Min(bound + 1, _frame.RecordCount - 1);
                                return;
                            case SeekMode.Floor:
                                _index = bound;
                                return;
                        }
                    }

                    bound--;
                    boundOffset = _frame.TimeSignal.GetOffset(bound);
                }

                _index = 0;
                return;
            }
            else
            {
                _index = bound;
                return;
            }
        }
    }
}
