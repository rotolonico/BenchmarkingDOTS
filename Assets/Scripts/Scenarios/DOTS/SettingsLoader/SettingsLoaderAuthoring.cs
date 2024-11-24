using Unity.Entities;
using UnityEngine;

namespace Scenarios.DOTS.SettingsLoader
{
    public class SettingsLoaderAuthoring : MonoBehaviour
    {
        public class Baker : Baker<SettingsLoaderAuthoring>
        {
            public override void Bake(SettingsLoaderAuthoring spawnerAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SettingsLoader());
            }
        }
    }
}