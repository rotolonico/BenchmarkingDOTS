using Scenarios.APIs;
using Unity.Entities;
using UnityEngine;

namespace Scenarios.DOTS.Scenario_1.SpheresSpawner
{
    public class SpheresSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject spherePrefab;

        public class Baker : Baker<SpheresSpawnerAuthoring>
        {
            public override void Bake(SpheresSpawnerAuthoring spawnerAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var config = new SpheresSpawner
                {
                    SpherePrefab = GetEntity(spawnerAuthoring.spherePrefab, TransformUsageFlags.Dynamic),
                };
                AddComponent(entity, config);
            }
        }
    }
}
