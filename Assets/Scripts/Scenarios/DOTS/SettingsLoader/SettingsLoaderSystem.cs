using Scenarios.APIs;
using Unity.Burst;
using Unity.Entities;

namespace Scenarios.DOTS.SettingsLoader
{
    [BurstCompile]
    public partial struct SettingsLoaderSystem : ISystem
    {
        private int _numSpheres;
        private int _spawnRadius;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state) => state.RequireForUpdate<SettingsLoader>();

        public void OnUpdate(ref SystemState state)
        {
            var config = SystemAPI.GetSingleton<SettingsLoader>();

            if (config.Initialized) return;
            
            config.Initialized = true;
            config.numSpheres = ScenarioSettingsAPIs.GetNumEntities();
            config.spawnRadius = ScenarioSettingsAPIs.GetSpawnRadius();
            config.benchmarkMode = ScenarioSettingsAPIs.IsBenchmarkMode();
            
            SystemAPI.SetSingleton(config);
        }
    }
}