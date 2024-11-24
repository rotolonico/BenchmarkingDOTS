using Scenarios.APIs;
using UnityEngine;

namespace Scenarios.NoDOTS.Scenario_1
{
    public class SpheresSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject spherePrefab;

        private void Start()
        {
            var spread = ScenarioSettingsAPIs.GetSpawnRadius();

            for (var i = 0; i < ScenarioSettingsAPIs.GetNumEntities(); i++)
            {
                var newSphere = Instantiate(spherePrefab,
                    new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread),
                        Random.Range(-spread, spread)), Quaternion.identity);
                newSphere.GetComponent<SphereHandler>().Initialize(Random.insideUnitSphere);
            }
        }
    }
}