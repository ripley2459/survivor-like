public class PlayerController : ACharacterController
{
    private PlayerActions _playerActions;
    public PlayerActions PlayerActions => _playerActions;
    private Notifications _notifications;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _notifications = GetComponentInChildren<Notifications>();
    }

    private void OnEnable()
    {
        //_playerActions.Human.Enable();
    }

    private void OnDisable()
    {
        //_playerActions.Human.Disable();
    }

    public void PushNotification(Notifications.Notification notification)
    {
        _notifications.PushNotification(notification);
    }
}