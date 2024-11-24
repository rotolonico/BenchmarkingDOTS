using Unity.Burst;
using Unity.Entities;

namespace Scenarios.DOTS.Scenario_2.Sphere
{
    [BurstCompile]
    public partial struct MoveSphereSystem : ISystem 
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var job = new MoveSphereSystemJob
            {
                DeltaTime = deltaTime
            };

            job.ScheduleParallel();
        }
    }
}