using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace Scenarios.DOTS.Scenario_3.Sphere
{
    [BurstCompile]
    public partial struct SphereMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) => state.RequireForUpdate<SimulationSingleton>();

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (sphereData, sphereMove, physicsVelocity, physicsCollider) in SystemAPI
                         .Query<RefRO<Sphere>, RefRW<SphereMove>, RefRW<PhysicsVelocity>, RefRW<PhysicsCollider>>())
            {
                if (sphereMove.ValueRO.Initialized) return;
                sphereMove.ValueRW.Initialized = true;
                
                var direction = sphereMove.ValueRO.Direction;
                var speed = sphereData.ValueRO.Speed;
                physicsVelocity.ValueRW.Linear = direction * speed;
                
            }
        }
    }
}