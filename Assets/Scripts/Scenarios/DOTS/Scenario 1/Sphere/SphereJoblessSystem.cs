using Scenarios.Handlers;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Scenarios.DOTS.Scenario_1.Sphere
{
    [BurstCompile]
    public partial class SphereJoblessSystem : SystemBase 
    {
        protected override void OnUpdate()
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            InternalUpdate(deltaTime, CameraHandler.currentCameraTransform.position);
        }
        
        [BurstCompile]
        private void InternalUpdate(float deltaTime, float3 cameraPosition)
        {
            Entities.WithNone<SphereJobTag>().ForEach((ref LocalTransform localTransform, ref Sphere sphere, ref URPMaterialPropertyBaseColor baseColor, in SphereMove sphereMoveData) =>
            {
                var distance = math.distance(localTransform.Position, cameraPosition);
                var speedMultiplier =  Mathf.Max(0.25f, 1 - distance * 0.01f);
                
                var delta = sphere.Speed * speedMultiplier * deltaTime * sphereMoveData.Direction;
                var newPosition = localTransform.Position + delta;
            
                if (math.distance(sphereMoveData.InitialPosition, newPosition) > sphere.Spread)
                    sphere.Speed *= -1;
                else
                    localTransform = localTransform.Translate(delta);
            
                var color = Color.HSVToRGB(speedMultiplier, 1, 1);
                baseColor.Value = new float4(color.r, color.g, color.b, 1);
            }).Schedule();
        }
    }
}