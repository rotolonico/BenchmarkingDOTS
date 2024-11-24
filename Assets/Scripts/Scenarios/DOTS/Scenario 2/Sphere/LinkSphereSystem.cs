using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Scenarios.DOTS.Scenario_2.Sphere
{
    [BurstCompile]
    public partial struct LinkSphereSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var parentQuery = SystemAPI.QueryBuilder().WithAll<LocalTransform, SphereJobTag>().Build();
            var sphereEntities = parentQuery.ToEntityArray(Allocator.TempJob);
            var sphereTransforms = parentQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);

            var numSpheres = sphereEntities.Length;
            
            var spherePositions = new NativeArray<float3>(numSpheres, Allocator.TempJob);
            var closestSphereIndices = new NativeArray<int>(numSpheres, Allocator.TempJob);
            var linkEntities = new NativeArray<Entity>(numSpheres, Allocator.TempJob);
            
            for (var i = 0; i < numSpheres; i++)
            {
                spherePositions[i] = sphereTransforms[i].Position;
                linkEntities[i] = GetLinkEntity(sphereEntities[i], ref state);
                if (linkEntities[i] == Entity.Null) return;
            }

            // Job to calculate closest spheres
            var calculateJob = new CalculateClosestSphereJob
            {
                SpherePositions = spherePositions,
                NumSpheres = numSpheres,
                ClosestSphereIndices = closestSphereIndices
            };
            
            var calculateHandle = calculateJob.Schedule(numSpheres, 64);

            // Set up ComponentLookup for LocalToWorld to modify link entities
            var localToWorldLookup = SystemAPI.GetComponentLookup<LocalToWorld>(isReadOnly: false);
            localToWorldLookup.Update(ref state);
            
            JobHandle combinedDependencies = JobHandle.CombineDependencies(calculateHandle, state.Dependency);

            // Job to update the LocalToWorld of the link entities based on closest sphere calculations
            var updateJob = new UpdateLinkTransformJob
            {
                ClosestSphereIndices = closestSphereIndices,
                SpherePositions = spherePositions,
                LocalToWorldLookup = localToWorldLookup,
                LinkEntities = linkEntities
            };
            
            JobHandle updateHandle = updateJob.Schedule(numSpheres, 64, combinedDependencies);

            // Combine with the system's current dependency chain
            state.Dependency = updateHandle;

            // Ensure proper disposal of NativeArrays after jobs are complete
            sphereEntities.Dispose(state.Dependency);
            sphereTransforms.Dispose(state.Dependency);
            spherePositions.Dispose(state.Dependency);
            closestSphereIndices.Dispose(state.Dependency);
            linkEntities.Dispose(state.Dependency);
        }
        
        private Entity GetLinkEntity(Entity sphereEntity, ref SystemState state)
        {
            var childrenBuffer = SystemAPI.GetBuffer<Child>(sphereEntity);
            return childrenBuffer[0].Value; // Assuming there's exactly one child link entity
        }
    }
}