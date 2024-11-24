using Unity.Entities;

namespace Scenarios.DOTS.Scenario_1.Sphere
{
    public struct Sphere : IComponentData
    {
        public float Speed;
        public float Spread;
    }
}