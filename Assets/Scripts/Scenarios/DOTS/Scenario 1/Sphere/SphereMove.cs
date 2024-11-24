using Unity.Entities;
using Unity.Mathematics;

namespace Scenarios.DOTS.Scenario_1.Sphere
{
    public struct SphereMove : IComponentData
    {
        public float3 InitialPosition;
        public float3 Direction;
    }
}