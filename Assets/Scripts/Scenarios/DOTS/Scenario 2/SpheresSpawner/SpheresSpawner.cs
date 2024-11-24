using Unity.Entities;

namespace Scenarios.DOTS.Scenario_2.SpheresSpawner
{
    public struct SpheresSpawner : IComponentData
    {
        public Entity SpherePrefab;
        public bool Initialized;
    }
}
