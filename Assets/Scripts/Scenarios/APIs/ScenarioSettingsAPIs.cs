using Scenarios.Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenarios.APIs
{
    public static class ScenarioSettingsAPIs
    {
        private static ScenarioSettings _currentScenarioSettings = new();
        
        public static void InitializeSettings(ScenarioSettings settings)
        {
            _currentScenarioSettings = new ScenarioSettings
            {
                numEntities = settings.numEntities,
                spawnRadius = settings.spawnRadius
            };
        }
        
        public static void InitializeSettingsWithBenchmark()
        {
            _currentScenarioSettings = new ScenarioSettings
            {
                benchmarkMode = true,
                numEntities = BenchmarkSettingsAPIs.GetSettings().benchmarkNumEntities,
                spawnRadius = 100
            };
        }
    
        public static int GetNumEntities() => _currentScenarioSettings.numEntities;

        public static int GetSpawnRadius() => _currentScenarioSettings.spawnRadius;

        public static void SetNumEntities(int value) => _currentScenarioSettings.numEntities = value;

        public static void SetSpawnRadius(int value) => _currentScenarioSettings.spawnRadius = value;

        public static bool IsBenchmarkMode() => _currentScenarioSettings.benchmarkMode;
    }
}
