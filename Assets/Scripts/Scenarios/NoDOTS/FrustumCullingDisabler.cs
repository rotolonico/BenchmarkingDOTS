using Scenarios.APIs;
using UnityEngine;

namespace Scenarios.NoDOTS
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FrustumCullingDisabler : MonoBehaviour
    {
        private MeshRenderer mesh;
        private Bounds bounds;
        
        private void Start()
        {
            if (!ScenarioSettingsAPIs.IsBenchmarkMode()) return;
            
            mesh = GetComponent<MeshRenderer>();
            bounds = new Bounds(Vector3.zero, Vector3.one * 1000);
            mesh.bounds = bounds;
        }
    }
}
