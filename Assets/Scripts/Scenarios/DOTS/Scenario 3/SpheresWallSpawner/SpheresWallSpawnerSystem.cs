using Scenarios.APIs;
using Scenarios.DOTS.Scenario_3.Sphere;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenarios.DOTS.Scenario_3.SpheresWallSpawner
{
    [UpdateAfter(typeof(SettingsLoader.SettingsLoaderSystem))]
    [BurstCompile]
    public partial struct SpheresWallSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpheresWallSpawner>();
            state.RequireForUpdate<SettingsLoader.SettingsLoader>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var config = SystemAPI.GetSingleton<SpheresWallSpawner>();
            var settings = SystemAPI.GetSingleton<SettingsLoader.SettingsLoader>();

            if (config.Initialized) return;
            config.Initialized = true;
            SystemAPI.SetSingleton(config);
            
            for (var i = 0; i < settings.numSpheres; i++)
            {
                var position = new float3(Random.Range(-settings.spawnRadius, settings.spawnRadius),
                    Random.Range(-settings.spawnRadius, settings.spawnRadius),
                    Random.Range(-settings.spawnRadius, settings.spawnRadius));

                var entity = state.EntityManager.Instantiate(config.SpherePrefab);

                state.EntityManager.SetComponentData(entity, new LocalTransform
                {
                    Position = position,
                    Rotation = Quaternion.identity,
                    Scale = 1f
                });
                
                state.EntityManager.AddComponentData(entity, new SphereMove
                {
                    Direction = new float3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))
                });
            }
            
            var wall = state.EntityManager.Instantiate(config.WallPrefab);
            state.EntityManager.AddComponentData(wall, new LocalTransform
            {
                Position = float3.zero,
                Rotation = Quaternion.identity,
                Scale = settings.spawnRadius
            });
            state.EntityManager.AddComponentData(wall, new WallTag());
        }
    }
}