using System;
using UnityEngine.Serialization;

namespace Scenarios.Objects
{
    [Serializable]
    public class BenchmarkSettings
    {
        public int benchmarkIncrement = 10000;
        public int benchmarkNumSnapshots = 10;
        public int benchmarkNumEntities = 10000;
        public int benchmarkSnapshotDuration = 5;
    }
}