using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;

namespace Scenarios.DOTS.Scenario_3.Sphere
{
    [BurstCompile]
    public partial struct SphereJoblessCollisionSystem : ISystem
    {
        private Simulation _simulation;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _simulation = SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation();
            var collisionEventStream = _simulation.CollisionEvents;

            foreach (var collisionEvent in collisionEventStream)
            {
                Entity entityA = collisionEvent.EntityA;
                Entity entityB = collisionEvent.EntityB;

                bool entityAIsSphere = SystemAPI.HasComponent<SphereJoblessTag>(entityA);
                bool entityBIsSphere = SystemAPI.HasComponent<SphereJoblessTag>(entityB);

                bool entityAIsWall = SystemAPI.HasComponent<WallTag>(entityA);
                bool entityBIsWall = SystemAPI.HasComponent<WallTag>(entityB);

                if (entityAIsSphere && entityBIsWall)
                {
                    SystemAPI.GetComponentRW<URPMaterialPropertyBaseColor>(entityA).ValueRW.Value =
                        new float4(1f, 1f, 0f, 1f);
                }
                else if (entityBIsSphere && entityAIsWall)
                {
                    SystemAPI.GetComponentRW<URPMaterialPropertyBaseColor>(entityB).ValueRW.Value =
                        new float4(1f, 1f, 0f, 1f);
                }

                if (entityAIsSphere && entityBIsSphere)
                {
                    SystemAPI.GetComponentRW<URPMaterialPropertyBaseColor>(entityA).ValueRW.Value =
                        new float4(0f, 1f, 0f, 1f);
                    SystemAPI.GetComponentRW<URPMaterialPropertyBaseColor>(entityB).ValueRW.Value =
                        new float4(0f, 1f, 0f, 1f);
                }
            }
        }
    }
}