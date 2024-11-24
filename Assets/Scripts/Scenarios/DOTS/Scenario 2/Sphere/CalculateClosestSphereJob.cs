using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Scenarios.DOTS.Scenario_2.Sphere
{
    [BurstCompile]
    public struct CalculateClosestSphereJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<float3> SpherePositions;
        [ReadOnly] public int NumSpheres;
        [WriteOnly] public NativeArray<int> ClosestSphereIndices;

        public void Execute(int index)
        {
            var currentParentPosition = SpherePositions[index];
            var minDistance = float.MaxValue;
            var closestParentIndex = -1;

            for (var i = 0; i < NumSpheres; i++)
            {
                if (i == index) continue; // Skip self-comparison

                var distance = math.distance(currentParentPosition, SpherePositions[i]);
                
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                closestParentIndex = i;
            }

            // Store the index of the closest parent
            ClosestSphereIndices[index] = closestParentIndex;
        }
    }
}
