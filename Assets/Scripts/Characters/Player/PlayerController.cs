public class PlayerController : ACharacterController
{
    public PlayerActions PlayerActions { get; private set; }

    public PlayerLevel PlayerLevel { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        PlayerLevel ??= GetComponent<PlayerLevel>();
    }
}