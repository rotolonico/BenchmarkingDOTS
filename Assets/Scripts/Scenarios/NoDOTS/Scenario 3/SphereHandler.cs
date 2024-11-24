using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenarios.NoDOTS.Scenario_3
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    public class SphereHandler : MonoBehaviour
    {
        [SerializeField] private float speed = 30;
        private Vector3 _direction;

        private Rigidbody _rigidbody;
        private MeshRenderer _mesh;

        public void Initialize(Vector3 direction)
        {
            _direction = direction;
            _rigidbody = GetComponent<Rigidbody>();
            
            _mesh = GetComponent<MeshRenderer>();
            _mesh.material.color = Color.red;
            
            _rigidbody.AddForce(_direction * speed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                _mesh.material.color = Color.yellow;
            }
            else if (other.gameObject.CompareTag("Sphere"))
            {
                _mesh.material.color = Color.green;
            }
        }
    }
}