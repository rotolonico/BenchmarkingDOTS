using Scenarios.APIs;
using TMPro;
using UnityEngine;

namespace Scenarios.Handlers
{
    public class CameraHandler : MonoBehaviour
    {
        public static Transform currentCameraTransform;
        
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float sprintMultiplier = 2.0f;
        [SerializeField] private float mouseSensitivity = 300.0f;

        private float _rotationX;
        private float _rotationY;
        private bool _isCursorLocked = true;
        
        private void Awake()
        {
            currentCameraTransform = transform;
            UnlockCursor();
        }

        private void Update()
        {
            if (_isCursorLocked)
                HandleMouseLook();

            if (_isCursorLocked)
                HandleMovement();

            if (Input.GetKeyDown(KeyCode.Escape) && !ScenarioSettingsAPIs.IsBenchmarkMode())
                ToggleCursorLock();
        }

        private void HandleMouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            _rotationX -= mouseY;
            _rotationY += mouseX;

            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
        }

        private void HandleMovement()
        {
            var moveSpeed = speed;

            if (Input.GetKey(KeyCode.LeftControl))
                moveSpeed *= sprintMultiplier;
            
            var right = new Vector3(transform.right.x, 0, transform.right.z);
            var forward = new Vector3(-right.z, 0f, right.x);
            
            right.Normalize();
            forward.Normalize();

            var move = right * Input.GetAxis("Horizontal") + forward * Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.Space))
                move += Vector3.up;

            if (Input.GetKey(KeyCode.LeftShift))
                move += Vector3.down;

            transform.position += move * (moveSpeed * Time.deltaTime);
        }

        private void ToggleCursorLock()
        {
            _isCursorLocked = !_isCursorLocked ? LockCursor() : UnlockCursor();
        }

        public bool LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _isCursorLocked = true;
            return _isCursorLocked;
        }

        private bool UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _isCursorLocked = false;
            return _isCursorLocked;
        }

        private void OnDestroy() => ResetCursor();

        private void ResetCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
