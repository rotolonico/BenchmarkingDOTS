using Unity.Entities;

namespace Scenarios.DOTS.Scenario_3.SpheresWallSpawner
{
    public struct SpheresWallSpawner : IComponentData
    {
        public Entity SpherePrefab;
        public Entity WallPrefab;
        public bool Initialized;
    }
}
