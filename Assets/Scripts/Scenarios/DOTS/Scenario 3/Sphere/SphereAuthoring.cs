using Scenarios.DOTS.FrustumCulling;
using Unity.Entities;
using UnityEngine;

namespace Scenarios.DOTS.Scenario_3.Sphere
{
    public class SphereAuthoring : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool usesJobs;

        public class Baker : Baker<SphereAuthoring>
        {
            public override void Bake(SphereAuthoring sphereAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var config = new Sphere
                {
                    Speed = sphereAuthoring.speed
                };
                AddComponent(entity, config);
                AddComponent(entity, typeof(FrustumCullingDisablerTag));

                AddComponent(entity, sphereAuthoring.usesJobs ? typeof(SphereJobTag) : typeof(SphereJoblessTag));
            }
        }
    }
}