using Scenarios.APIs;
using Unity.Entities;
using UnityEngine;

namespace Scenarios.DOTS.Scenario_3.SpheresWallSpawner
{
    public class SpheresWallSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject spherePrefab;
        [SerializeField] private GameObject wallPrefab;

        public class Baker : Baker<SpheresWallSpawnerAuthoring>
        {
            public override void Bake(SpheresWallSpawnerAuthoring wallSpawnerAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var config = new SpheresWallSpawner
                {
                    SpherePrefab = GetEntity(wallSpawnerAuthoring.spherePrefab, TransformUsageFlags.Dynamic),
                    WallPrefab = GetEntity(wallSpawnerAuthoring.wallPrefab, TransformUsageFlags.Dynamic)
                };
                AddComponent(entity, config);
            }
        }
    }
}
