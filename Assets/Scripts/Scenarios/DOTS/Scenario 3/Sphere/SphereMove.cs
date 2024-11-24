using Unity.Entities;
using Unity.Mathematics;

namespace Scenarios.DOTS.Scenario_3.Sphere
{
    public struct SphereMove : IComponentData
    {
        public float3 Direction;
        public bool Initialized;
    }
}