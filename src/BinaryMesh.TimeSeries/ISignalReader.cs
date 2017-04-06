using System;

namespace BinaryMesh.TimeSeries
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISignalReader
    {
        ISignal Signal { get; }

        TimeSpan CurrentTime { get; }

        bool MoveForward(TimeSpan offset);

        double GetReal();

        string GetString();
    }
}
