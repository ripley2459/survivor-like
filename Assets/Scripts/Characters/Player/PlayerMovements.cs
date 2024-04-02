using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [Header("Movements")] 
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private float _crouchSpeed = 1.5f;
    [SerializeField] private float _crouchTransitionSpeed = 1.5f;
    [SerializeField] private float _height = 1.75f;
    [SerializeField] private float _crouchingHeight = 1.75f;
    [Header("Camera")] 
    [SerializeField] private float _cameraSpeed = 10f;
    private Camera _camera;
    private PlayerController _playerController;
    private CharacterController _characterController;
    private Vector3 _direction;
    private float _rotationX = 0;

    /// <summary>
    /// Mouse position delta on the screen.
    /// </summary>
    public Vector2 MousePos => Mouse.current.delta.ReadValue();

    /// <summary>
    /// Where the player wants to go.
    /// </summary>
    public Vector2 Direction => _playerController.PlayerActions.Human.Movements.ReadValue<Vector2>();

    /// <summary>
    /// Is the character actually crouching.
    /// </summary>
    public bool Crouching => _playerController.PlayerActions.Human.Crouch.ReadValue<float>() > 0f;

    /// <summary>
    /// Is the character actually running.
    /// </summary>
    public bool Running => _playerController.PlayerActions.Human.Run.ReadValue<float>() > 0f && Direction.y > 0f && !Crouching;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        _playerController = GetComponent<PlayerController>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
        Crouch();
        Move();
    }

    private void Crouch()
    {
        _characterController.height = Crouching ? _crouchingHeight : _height;
        // _characterController.center = new Vector3(0f, Crouching ? -0.5f : 0f, 0f);
    }

    /// <summary>
    /// Camera control.
    /// </summary>
    private void Look()
    {
        _rotationX += -MousePos.y * _cameraSpeed * Time.deltaTime;
        _rotationX = Mathf.Clamp(_rotationX, -90, 90);
        _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, MousePos.x * _cameraSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// Moves the character.
    /// </summary>
    private void Move()
    {
        Vector3 forward = Vector3.Cross(transform.up, -_camera.transform.right).normalized;
        Vector3 right = Vector3.Cross(transform.up, _camera.transform.forward).normalized;
        float speed = Crouching ? _crouchSpeed : Running ? _runSpeed : _walkSpeed;
        _direction = (forward * Direction.y + right * Direction.x) * (speed * Time.deltaTime);
        _characterController.Move(_direction);
    }
}