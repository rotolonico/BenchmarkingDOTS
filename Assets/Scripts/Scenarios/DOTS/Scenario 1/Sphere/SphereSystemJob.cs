using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Scenarios.DOTS.Scenario_1.Sphere
{
    [BurstCompile]
    public partial struct SphereSystemJob : IJobEntity
    {
        public float DeltaTime;
        public float3 CameraPosition;

        [BurstCompile]
        private void Execute(ref LocalTransform localTransform, ref Sphere sphere,
            ref URPMaterialPropertyBaseColor baseColor, in SphereMove sphereMove, in SphereJobTag sphereJobTag)
        {
            var speedMultiplier = CalculateSpeedMultiplier(localTransform.Position);
            var delta = sphere.Speed * speedMultiplier * DeltaTime * sphereMove.Direction;
            var newPosition = localTransform.Position + delta;

            if (math.distance(sphereMove.InitialPosition, newPosition) > sphere.Spread)
                sphere.Speed *= -1;
            else
                localTransform = localTransform.Translate(delta);

            var color = Color.HSVToRGB(speedMultiplier, 1, 1);
            baseColor.Value = new float4(color.r, color.g, color.b, 1);
        }

        [BurstCompile]
        private float CalculateCameraDistance(float3 entityPosition) => math.distance(entityPosition, CameraPosition);

        [BurstCompile]
        private float CalculateSpeedMultiplier(float3 entityPosition)
        {
            var distance = CalculateCameraDistance(entityPosition);
            return Mathf.Max(0.25f, 1 - distance * 0.01f);
        }
    }
}