public class MonsterController : ACharacterController
{
    private void Start()
    {
        Invoke(nameof(Kill), 2f);
    }

    private void Kill()
    {
        OnKilled?.Invoke(this);
    }
}