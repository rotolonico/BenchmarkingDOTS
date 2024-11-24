using System;

namespace Scenarios.Objects
{
    [Serializable]
    public class BenchmarkResults
    {
        public string scenarioName;
        public BenchmarkSettings settings;
        public BenchmarkResult[] results;
    }
}