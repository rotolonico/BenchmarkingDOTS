using Unity.Entities;

namespace Scenarios.DOTS.Scenario_3.Sphere
{
    public struct Sphere : IComponentData
    {
        public float Speed;
        public float Spread;
    }
}