using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Scenarios.DOTS.Scenario_2.Sphere
{
    [BurstCompile]
    public partial struct LinkSphereJoblessSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var parentQuery = SystemAPI.QueryBuilder().WithAll<LocalTransform, SphereJoblessTag>().Build();
            
            // Set up ComponentLookup for LocalToWorld to modify link entities
            var localToWorldLookup = SystemAPI.GetComponentLookup<LocalToWorld>(isReadOnly: false);
            localToWorldLookup.Update(ref state);

            var sphereEntities = parentQuery.ToEntityArray(Allocator.Temp);
            var sphereTransforms = parentQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);

            var numSpheres = sphereEntities.Length;

            for (var i = 0; i < numSpheres; i++)
            {
                var linkEntity = GetLinkEntity(sphereEntities[i], ref state);

                // Jobless version of CalculateClosestSphereJob
                var currentParentPosition = sphereTransforms[i].Position;
                var minDistance = float.MaxValue;
                var closestParentIndex = -1;

                for (var j = 0; j < numSpheres; j++)
                {
                    if (j == i) continue; // Skip self-comparison

                    var distance = math.distance(currentParentPosition, sphereTransforms[j].Position);

                    if (!(distance < minDistance)) continue;
                    minDistance = distance;
                    closestParentIndex = j;
                }
                
                // Jobless version of UpdateLinkTransformJob
                var closestParentPosition = sphereTransforms[closestParentIndex].Position;

                if (!localToWorldLookup.HasComponent(linkEntity)) continue;

                var midpointPosition = (currentParentPosition + closestParentPosition) * 0.5f;
                var directionToClosestParent = math.normalize(closestParentPosition - currentParentPosition);
                
                var scale = new float3(0.1f, 0.1f, minDistance); // Scale more along Z to represent the link
                var lookRotation = quaternion.LookRotationSafe(directionToClosestParent, math.up());

                var scaleMatrix = float4x4.Scale(scale);
                var translationRotationMatrix = new float4x4(lookRotation, midpointPosition);

                var localToWorld = math.mul(translationRotationMatrix, scaleMatrix);

                // Set the child's LocalToWorld matrix
                localToWorldLookup[linkEntity] = new LocalToWorld { Value = localToWorld };
            }

            sphereEntities.Dispose();
            sphereTransforms.Dispose();
        }

        private Entity GetLinkEntity(Entity sphereEntity, ref SystemState state)
        {
            var childrenBuffer = SystemAPI.GetBuffer<Child>(sphereEntity);
            return childrenBuffer[0].Value; // Assuming there's exactly one child link entity
        }
    }
}