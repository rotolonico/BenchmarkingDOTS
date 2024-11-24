using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Rendering;

namespace Scenarios.DOTS.Scenario_3.Sphere
{
    [BurstCompile]
    public partial struct SphereSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var collisionJob = new SphereCollisionJob
            {
                ColorLookup = SystemAPI.GetComponentLookup<URPMaterialPropertyBaseColor>(),
                SphereTagLookup = SystemAPI.GetComponentLookup<SphereJobTag>(true),
                WallTagLookup = SystemAPI.GetComponentLookup<WallTag>(true)
            };
            
            collisionJob.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();
        }
    }
}