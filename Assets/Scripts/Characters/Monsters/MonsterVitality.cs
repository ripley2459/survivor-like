public class MonsterVitality : ACharacterVitality
{
    private void OnEnable()
    {
        // Invoke(nameof(KillAfter), 3f);
    }

    private void KillAfter()
    {
        Damage(999);
    }
}