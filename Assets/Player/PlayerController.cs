using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _rotationSpeed = 10f; // degrees per second
    [SerializeField]
    private Transform _cameraTransform;

    private Rigidbody _rigidbody;
    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;
    private bool _jumpPressed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputActions = new InputSystem_Actions();

        // binding input actions
        _inputActions.Player.Move.performed += (ctx) => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += (ctx) => _moveInput = Vector2.zero;
        _inputActions.Player.Jump.performed += (ctx) => _jumpPressed = true;

        if (_cameraTransform == null)
            _cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Rotate();
    }

    private void Move()
    {
        // Get camera forward & right (flattened on ground plane)
        Vector3 camForward = _cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = _cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // Convert input into camera-relative direction
        Vector3 direction = camForward * _moveInput.y + camRight * _moveInput.x;
        Vector3 velocity = direction.normalized * _moveSpeed;

        // Keep vertical velocity from physics
        velocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = velocity;
    }

    private void Jump()
    {
        if (_jumpPressed && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        _jumpPressed = false; // reset
    }

    private void Rotate()
    {
        // Rotate only if moving
        Vector3 camForward = _cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = _cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 direction = camForward * _moveInput.y + camRight * _moveInput.x;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.fixedDeltaTime
            );
        }
    }

    private bool IsGrounded()
    {
        // Simple ground check
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
