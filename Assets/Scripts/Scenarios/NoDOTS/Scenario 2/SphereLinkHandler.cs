using UnityEngine;

namespace Scenarios.NoDOTS.Scenario_2
{
    public class SphereLinkHandler : MonoBehaviour
    {
        [SerializeField] private Transform linkerTransform;
        
        private GameObject[] _spheres;

        public void Initialize(GameObject[] spawnedSpheres)
        {
            _spheres = spawnedSpheres;
            
            linkerTransform.GetComponent<MeshRenderer>().material.color = Color.green;
        }

        private void Update()
        {
            var sphereWithMinDistance = GetSphereWithMinDistance();
            
            // Create a link between the current sphere and the sphere with the minimum distance
            linkerTransform.position = (transform.position + sphereWithMinDistance.transform.position) * 0.5f;
            linkerTransform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(transform.position, sphereWithMinDistance.transform.position));
            linkerTransform.LookAt(sphereWithMinDistance.transform.position);
        }

        private GameObject GetSphereWithMinDistance()
        {
            var minDistance = float.MaxValue;
            var minDistanceSphere = _spheres[0];

            foreach (var sphere in _spheres)
            {
                if (sphere.GetInstanceID() == gameObject.GetInstanceID()) continue;
                
                var distance = Vector3.Distance(transform.position, sphere.transform.position);

                if (!(distance < minDistance)) continue;
                minDistance = distance;
                minDistanceSphere = sphere;
            }

            return minDistanceSphere;
        }
    }
}