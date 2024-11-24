using Scenarios.APIs;
using Scenarios.DOTS.Scenario_2.Sphere;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenarios.DOTS.Scenario_2.SpheresSpawner
{
    [UpdateAfter(typeof(SettingsLoader.SettingsLoaderSystem))]
    [BurstCompile]
    public partial struct SpheresSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpheresSpawner>();
            state.RequireForUpdate<SettingsLoader.SettingsLoader>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var config = SystemAPI.GetSingleton<SpheresSpawner>();
            var settings = SystemAPI.GetSingleton<SettingsLoader.SettingsLoader>();

            if (config.Initialized) return;
            
            config.Initialized = true;
            SystemAPI.SetSingleton(config);
            
            for (var i = 0; i < settings.numSpheres; i++)
            {
                var position = new float3(Random.Range(-settings.spawnRadius, settings.spawnRadius),
                    Random.Range(-settings.spawnRadius, settings.spawnRadius),
                    Random.Range(-settings.spawnRadius, settings.spawnRadius));
                
                var speed = Random.Range(2f, 10f);

                var entity = state.EntityManager.Instantiate(config.SpherePrefab);

                state.EntityManager.SetComponentData(entity, new LocalTransform
                {
                    Position = position,
                    Rotation = Quaternion.identity,
                    Scale = 1f
                });
                
                state.EntityManager.AddComponentData(entity, new SphereMove
                {
                    InitialPosition = position,
                    Direction = new float3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)),
                    Speed = speed
                });
            }
        }
    }
}