using Scenarios.DOTS.FrustumCulling;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenarios.DOTS.Scenario_1.Sphere
{
    public class SphereAuthoring : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float spread;
        [SerializeField] private bool usesJobs;

        public class Baker : Baker<SphereAuthoring>
        {
            public override void Bake(SphereAuthoring sphereAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var config = new Sphere
                {
                    Speed = sphereAuthoring.speed,
                    Spread = sphereAuthoring.spread
                };
                AddComponent(entity, config);
                AddComponent(entity, typeof(FrustumCullingDisablerTag));

                AddComponent(entity, sphereAuthoring.usesJobs ? typeof(SphereJobTag) : typeof(SphereJoblessTag));
            }
        }
    }
}