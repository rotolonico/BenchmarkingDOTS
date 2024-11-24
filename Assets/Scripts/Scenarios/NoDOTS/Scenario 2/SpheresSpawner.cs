using Scenarios.APIs;
using UnityEngine;

namespace Scenarios.NoDOTS.Scenario_2
{
    public class SpheresSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject spherePrefab;

        private GameObject[] _spheres;

        private void Start()
        {
            var spread = ScenarioSettingsAPIs.GetSpawnRadius();

            _spheres = new GameObject[ScenarioSettingsAPIs.GetNumEntities()];

            for (var i = 0; i < ScenarioSettingsAPIs.GetNumEntities(); i++)
            {
                var newSphere = Instantiate(spherePrefab,
                    new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread),
                        Random.Range(-spread, spread)), Quaternion.identity);
                
                _spheres[i] = newSphere;
                
                newSphere.GetComponent<SphereMovementHandler>().Initialize(Random.insideUnitSphere, Random.Range(10f, 30f));
                newSphere.GetComponent<SphereLinkHandler>().Initialize(_spheres);
            }
        }
    }
}