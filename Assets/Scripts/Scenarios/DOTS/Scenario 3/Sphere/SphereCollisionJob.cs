using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;

namespace Scenarios.DOTS.Scenario_3.Sphere
{
    [BurstCompile]
    public struct SphereCollisionJob : ICollisionEventsJob
    {
        [NativeDisableParallelForRestriction] public ComponentLookup<URPMaterialPropertyBaseColor> ColorLookup;
        [ReadOnly] public ComponentLookup<SphereJobTag> SphereTagLookup;
        [ReadOnly] public ComponentLookup<WallTag> WallTagLookup;

        [BurstCompile]
        public void Execute(CollisionEvent collisionEvent)
        {
            Entity entityA = collisionEvent.EntityA;
            Entity entityB = collisionEvent.EntityB;

            bool entityAIsSphere = SphereTagLookup.HasComponent(entityA);
            bool entityBIsSphere = SphereTagLookup.HasComponent(entityB);

            bool entityAIsWall = WallTagLookup.HasComponent(entityA);
            bool entityBIsWall = WallTagLookup.HasComponent(entityB);

            if (entityAIsSphere && entityBIsWall)
            {
                var color = ColorLookup[entityA];
                color.Value = new float4(1f, 1f, 0f, 1f);
                ColorLookup[entityA] = color;
            }
            else if (entityBIsSphere && entityAIsWall)
            {
                var color = ColorLookup[entityB];
                color.Value = new float4(1f, 1f, 0f, 1f);
                ColorLookup[entityB] = color;
            }

            if (entityAIsSphere && entityBIsSphere)
            {
                var colorA = ColorLookup[entityA];
                var colorB = ColorLookup[entityB];
                colorA.Value = new float4(0f, 1f, 0f, 1f);
                colorB.Value = new float4(0f, 1f, 0f, 1f);
                ColorLookup[entityA] = colorA;
                ColorLookup[entityB] = colorB;
            }
        }
    }
}