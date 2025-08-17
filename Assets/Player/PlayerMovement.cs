using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private Vector2 _moveAmount;
    private Vector2 _lookAmount;
    #region Serialized Fields
    [SerializeField]
    private InputActionAsset _inputAction;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed = 5f;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    private void OnEnable()
    {
        _inputAction.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        _inputAction.FindActionMap("Player").Disable();
    }


    // Update is called once per frame
    private void Update()
    {
        _moveAmount = _moveAction.ReadValue<Vector2>();
        _lookAmount = _lookAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Moving();
        Rotating();
    }

    private void Moving()
    {
        _rigidbody.MovePosition(_rigidbody.position + _speed * Time.deltaTime * new Vector3(_moveAmount.x, 0, _moveAmount.y));
    }

    private void Rotating()
    {
        if (_moveAmount.y != 0)
        {
            float rotationAmount = _lookAmount.x * _rotationSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }
    }
}
