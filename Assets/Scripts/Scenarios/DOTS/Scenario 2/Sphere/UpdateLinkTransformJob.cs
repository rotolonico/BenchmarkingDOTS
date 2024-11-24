using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Scenarios.DOTS.Scenario_2.Sphere
{
    [BurstCompile]
    public struct UpdateLinkTransformJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<int> ClosestSphereIndices; // Closest sphere index for each parent
        [ReadOnly] public NativeArray<float3> SpherePositions;
        public NativeArray<Entity> LinkEntities; // Array of child entities corresponding to the parents

        [NativeDisableParallelForRestriction]
        public ComponentLookup<LocalToWorld>
            LocalToWorldLookup; // ComponentLookup for modifying children LocalTransforms


        public void Execute(int index)
        {
            var closestParentIndex = ClosestSphereIndices[index];

            var currentParentPosition = SpherePositions[index];
            var closestParentPosition = SpherePositions[closestParentIndex];

            var childEntity = LinkEntities[index];

            if (!LocalToWorldLookup.HasComponent(childEntity)) return;
            
            var midpointPosition = (currentParentPosition + closestParentPosition) * 0.5f;
            var distance = math.distance(currentParentPosition, closestParentPosition);
            var directionToClosestParent = math.normalize(closestParentPosition - currentParentPosition);
            
            var scale = new float3(0.1f, 0.1f, distance); // Scale more along Z to represent the link
            var lookRotation = quaternion.LookRotationSafe(directionToClosestParent, math.up());
            
            var scaleMatrix = float4x4.Scale(scale);
            var translationRotationMatrix = new float4x4(lookRotation, midpointPosition);
            
            var localToWorld = math.mul(translationRotationMatrix, scaleMatrix);

            // Set the child's LocalToWorld matrix
            LocalToWorldLookup[childEntity] = new LocalToWorld { Value = localToWorld };
        }
    }
}