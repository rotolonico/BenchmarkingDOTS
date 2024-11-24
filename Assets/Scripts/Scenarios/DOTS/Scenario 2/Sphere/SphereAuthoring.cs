using Scenarios.DOTS.FrustumCulling;
using Unity.Entities;
using UnityEngine;

namespace Scenarios.DOTS.Scenario_2.Sphere
{
    public class SphereAuthoring : MonoBehaviour
    {
        [SerializeField] private float spread;
        [SerializeField] private bool usesJobs;

        public class Baker : Baker<SphereAuthoring>
        {
            public override void Bake(SphereAuthoring sphereAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var config = new Sphere
                {
                    Spread = sphereAuthoring.spread
                };
                AddComponent(entity, config);
                AddComponent(entity, typeof(FrustumCullingDisablerTag));

                AddComponent(entity, sphereAuthoring.usesJobs ? typeof(SphereJobTag) : typeof(SphereJoblessTag));
            }
        }
    }
}