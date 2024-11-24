using System.Linq;
using Unity.Entities;
using UnityEngine;

namespace Scenarios.NoDOTS
{
    public class DisableECSInScene : MonoBehaviour
    {
        private void Awake() => DisableAllECSSystems();
        
        private void OnDestroy() => EnableAllECSSystems();

        private static void DisableAllECSSystems()
        {
            var world = World.DefaultGameObjectInjectionWorld;

            if (world == null) return;
            
            foreach (var system in world.Systems) system.Enabled = false;
        }
    
        private static void EnableAllECSSystems()
        {
            var world = World.DefaultGameObjectInjectionWorld;

            if (world == null) return;
            
            foreach (var system in world.Systems) system.Enabled = true;
        }
    }
}
