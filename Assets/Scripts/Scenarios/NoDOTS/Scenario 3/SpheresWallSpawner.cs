using Scenarios.APIs;
using UnityEngine;

namespace Scenarios.NoDOTS.Scenario_3
{
    public class SpheresWallSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject spherePrefab;
        [SerializeField] private GameObject wallPrefab;

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
            
            var wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
            wall.transform.localScale *= spread;
        }
    }
}