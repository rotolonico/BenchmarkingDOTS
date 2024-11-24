using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Scenarios.DOTS.Scenario_2.Sphere
{
    [BurstCompile]
    public partial struct MoveSphereJoblessSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (localTransform, sphere, sphereMove) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRW<Sphere>, RefRW<SphereMove>>().WithAll<SphereJoblessTag>())
            {
                var delta = deltaTime * sphereMove.ValueRO.Speed * sphereMove.ValueRO.Direction;
                var newPosition = localTransform.ValueRO.Position + delta;

                if (math.distance(sphereMove.ValueRO.InitialPosition, newPosition) > sphere.ValueRO.Spread)
                    sphereMove.ValueRW.Speed *= -1;
                else
                    localTransform.ValueRW = localTransform.ValueRW.Translate(delta);
            }
        }
    }
}