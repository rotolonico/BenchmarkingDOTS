using Unity.Entities;

namespace Scenarios.DOTS.Scenario_1.SpheresSpawner
{
    public struct SpheresSpawner : IComponentData
    {
        public Entity SpherePrefab;
        public bool Initialized;
    }
}
