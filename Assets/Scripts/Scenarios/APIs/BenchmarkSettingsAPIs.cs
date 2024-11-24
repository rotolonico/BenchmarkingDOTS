using System;
using System.Collections.Generic;
using Scenarios.Objects;
using UnityEngine;

namespace Scenarios.APIs
{
    public static class BenchmarkSettingsAPIs
    {
        private static BenchmarkSettings _currentBenchmarkSettings = new();
        
        public static void SaveSettings(BenchmarkSettings settings)
        {
            _currentBenchmarkSettings = settings;
            PlayerPrefs.SetString("BenchmarkSettings", JsonUtility.ToJson(_currentBenchmarkSettings));
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadSettingsOnStart()
        {
            if (PlayerPrefs.HasKey("BenchmarkSettings")) _currentBenchmarkSettings = JsonUtility.FromJson<BenchmarkSettings>(PlayerPrefs.GetString("BenchmarkSettings", ""));
            _currentBenchmarkSettings ??= new BenchmarkSettings();
        }
        
        public static BenchmarkSettings GetSettings() => _currentBenchmarkSettings;
        
        public static string CalculateBenchmarkCompleteDuration() => ReadableTime(_currentBenchmarkSettings.benchmarkNumSnapshots * _currentBenchmarkSettings.benchmarkSnapshotDuration * 1000);
        
        private static string ReadableTime(int milliseconds)
        {
            var parts = new List<string>();

            var t = TimeSpan.FromMilliseconds(milliseconds);

            Add(t.Days, "d");
            Add(t.Hours, "h");
            Add(t.Minutes, "m");
            Add(t.Seconds, "s");
            Add(t.Milliseconds, "ms");

            return string.Join(" ", parts);

            void Add(int val, string unit)
            {
                if (val > 0) parts.Add(val + unit);
            }
        }
    }
}