using Scenarios.Handlers;
using Unity.Entities;

namespace Scenarios.DOTS.Scenario_1.Sphere
{
    public partial class SphereSystem : SystemBase 
    {
        protected override void OnUpdate()
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var job = new SphereSystemJob
            {
                DeltaTime = deltaTime,
                CameraPosition = CameraHandler.currentCameraTransform.position
            };

            job.ScheduleParallel();
        }
    }
}