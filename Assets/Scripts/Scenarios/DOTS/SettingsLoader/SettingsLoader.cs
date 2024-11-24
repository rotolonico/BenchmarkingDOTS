using Unity.Entities;

namespace Scenarios.DOTS.SettingsLoader
{
    public struct SettingsLoader : IComponentData
    {
        public bool Initialized;
        
        public int numSpheres;
        public int spawnRadius;
        public bool benchmarkMode;
    }
}