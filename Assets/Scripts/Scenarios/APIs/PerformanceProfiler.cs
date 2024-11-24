using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

namespace Scenarios.APIs
{
    public class PerformanceProfiler : MonoBehaviour
    {
        public static PerformanceProfiler Instance { get; private set; }
        
        private readonly List<float> fpsList = new();
        
        private float elapsedTime;
        private float timeInterval;
        private bool profilingActive;
        private Stopwatch stopwatch;
        private string logFilePath;
        
        private void Awake() => Instance = this;

        public void StartProfiling(float interval = 0.05f)
        {
            profilingActive = true;
            timeInterval = interval;
            elapsedTime = 0.0f;
            fpsList.Clear();
        }

        private void Update()
        {
            if (!profilingActive) return;
            elapsedTime += Time.unscaledDeltaTime;

            if (!(elapsedTime >= timeInterval)) return;
            CollectData();
            elapsedTime = 0.0f;
        }

        private void CollectData()
        {
            var fps = 1.0f / Time.unscaledDeltaTime;
            fpsList.Add(fps);
        }

        public float StopProfiling()
        {
            profilingActive = false;
            return GenerateAggregatedData();
        }
        
        private float GenerateAggregatedData()
        {
            // Not enough data to generate an average, scene is jammed
            if (fpsList.Count < 4)
                return 0.0f;
            
            var fpsSum = fpsList.Sum();
            var averageFps = fpsSum / fpsList.Count;
            
            return averageFps;
        }
    }
}
