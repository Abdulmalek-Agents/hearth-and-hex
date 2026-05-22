using UnityEngine;
using UnityEngine.InputSystem;

namespace HearthAndHex.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runMultiplier = 1.6f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private Transform cameraTransform;
        private CharacterController _cc;
        private Vector2 _moveInput;
        private bool _running;
        private float _yVelocity;

        private void Awake() => _cc = GetComponent<CharacterController>();
        public void OnMove(InputAction.CallbackContext ctx) => _moveInput = ctx.ReadValue<Vector2>();
        public void OnRun(InputAction.CallbackContext ctx) => _running = ctx.ReadValueAsButton();

        private void Update()
        {
            var camFwd = cameraTransform ? cameraTransform.forward : Vector3.forward;
            var camRight = cameraTransform ? cameraTransform.right : Vector3.right;
            camFwd.y = 0; camRight.y = 0; camFwd.Normalize(); camRight.Normalize();
            var dir = camFwd * _moveInput.y + camRight * _moveInput.x;
            float speed = walkSpeed * (_running ? runMultiplier : 1f);
            _yVelocity = _cc.isGrounded ? -1f : _yVelocity + gravity * Time.deltaTime;
            _cc.Move((dir * speed + Vector3.up * _yVelocity) * Time.deltaTime);
            if (dir.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 12f * Time.deltaTime);
        }
    }
}
