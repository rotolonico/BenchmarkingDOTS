using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Scenarios.DOTS.Scenario_2.Sphere
{
    [BurstCompile]
    public partial struct MoveSphereSystemJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ref LocalTransform localTransform, ref Sphere sphere, ref SphereMove sphereMove, in SphereJobTag sphereJobTag)
        {
            var delta = DeltaTime * sphereMove.Speed * sphereMove.Direction;
            var newPosition = localTransform.Position + delta;

            if (math.distance(sphereMove.InitialPosition, newPosition) > sphere.Spread)
                sphereMove.Speed *= -1;
            else
                localTransform = localTransform.Translate(delta);
        }
    }
}