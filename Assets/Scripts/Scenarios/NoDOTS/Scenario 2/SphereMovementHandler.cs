using UnityEngine;

namespace Scenarios.NoDOTS.Scenario_2
{
    public class SphereMovementHandler : MonoBehaviour
    {
        [SerializeField] private float spread = 10f;

        private Vector3 _initialPosition;
        private Vector3 _direction;
        private float _speed;

        public void Initialize(Vector3 direction, float speed)
        {
            _initialPosition = transform.position;
            _direction = direction;
            _speed = speed;

            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        private void Update()
        {
            var newPosition = transform.position + _speed * Time.deltaTime * _direction;
            if (Vector3.Distance(_initialPosition, newPosition) > spread)
                _speed *= -1;
            else
                transform.position = newPosition;
            
        }
    }
}