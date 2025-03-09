using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ACharacterController
{
    [SerializeField] private Camera _camera;

    public Camera Camera => _camera;

    public PlayerActions PlayerActions { get; private set; }

    public PlayerLevel PlayerLevel { get; private set; }

    /// <summary>
    /// Left click.
    /// </summary>
    public bool LeftClick => PlayerActions.Human.LeftClick.IsPressed();

    /// <summary>
    /// Right click.
    /// </summary>
    public bool RightClick => PlayerActions.Human.RightClick.IsPressed();

    /// <summary>
    /// Mouse position delta on the screen.
    /// </summary>
    public Vector2 MousePos => Mouse.current.delta.ReadValue();

    private void OnEnable()
    {
        PlayerActions.Human.Enable();
    }

    private void OnDisable()
    {
        PlayerActions.Human.Disable();
    }

    protected override void Awake()
    {
        base.Awake();

        PlayerActions = new PlayerActions();
        PlayerLevel ??= GetComponent<PlayerLevel>();
    }
}