using UnityEngine;
using UnityEngine.Serialization;

namespace Scenarios.NoDOTS.Scenario_1
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SphereHandler : MonoBehaviour
    {
        [SerializeField] private float speed = 30f;
        [SerializeField] private float spread = 10f;
        
        private Vector3 _initialPosition;
        private Vector3 _direction;

        private Camera _mainCamera;

        private MeshRenderer _mesh;

        public void Initialize(Vector3 direction)
        {
            _initialPosition = transform.position;
            _direction = direction;

            _mainCamera = Camera.main;

            _mesh = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            var speedMultiplier = CalculateSpeedMultiplier();
            var newPosition = transform.position + speed * speedMultiplier * Time.deltaTime * _direction;
            if (Vector3.Distance(_initialPosition, newPosition) > spread)
                speed *= -1;
            else
                transform.position = newPosition;
            
            _mesh.material.color = Color.HSVToRGB(speedMultiplier, 1, 1);
        }

        private float CalculateCameraDistance() => Vector3.Distance(_mainCamera.transform.position, transform.position);

        private float CalculateSpeedMultiplier()
        {
            var distance = CalculateCameraDistance();
            return Mathf.Max(0.25f, 1 - distance * 0.01f);
        }
    }
}