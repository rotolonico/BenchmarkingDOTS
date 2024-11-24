using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;

namespace Scenarios.DOTS.FrustumCulling
{
    [UpdateAfter(typeof(SettingsLoader.SettingsLoaderSystem))]
    [BurstCompile]
    public partial struct FrustumCullingDisablerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) => state.RequireForUpdate<SettingsLoader.SettingsLoader>();

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var settings = SystemAPI.GetSingleton<SettingsLoader.SettingsLoader>();
            
            if (!settings.benchmarkMode) return;
            
            foreach (var (bounds, disabled) in
                     SystemAPI.Query<RefRW<RenderBounds>, RefRW<FrustumCullingDisablerTag>>())
            {
                if (disabled.ValueRO.Initialized) continue;
                disabled.ValueRW.Initialized = true;
                
                bounds.ValueRW.Value = new Unity.Mathematics.AABB
                {
                    Center = new Unity.Mathematics.float3(0, 0, 0),
                    Extents = new Unity.Mathematics.float3(1000, 1000, 1000)
                };
            }
        }
    }
}