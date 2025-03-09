using UnityEngine;

public class MonsterVitality : ACharacterVitality
{
    [SerializeField] private PoolingManager.PoolableObject _displayableText;

    private void Awake()
    {
        OnDamaged += ShowDamage;
    }

    private void OnEnable()
    {
        // Invoke(nameof(KillAfter), 3f);
    }

    private void ShowDamage(ACharacterController controller, int damage)
    {
        var gameObject = PoolingManager.Instance.Spawn(ref _displayableText, Vector3.zero, Quaternion.identity);

        if (gameObject.TryGetComponent<DisplayDamage>(out DisplayDamage prompt))
        {
            prompt.transform.SetParent(transform);
            print("Before: " + prompt.transform.position);
            prompt.transform.position = new Vector3(0f, 1.275f, 0f);
            print("After: " + prompt.transform.position);
            prompt.SetDamage(damage);
        }
    }

    private void KillAfter()
    {
        Damage(999);
    }
}