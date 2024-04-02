using UnityEngine;

public class MonsterDamage : ACharacterDamage
{
    [SerializeField] protected int _damage;
    protected ACharacterController _characterController;
    
    public int Damage => _damage;

    private void Awake()
    {
        _characterController ??= GetComponentInParent<ACharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ACharacterVitality>(out ACharacterVitality vitality) && vitality.gameObject.layer != _characterController.Team)
        {
            vitality.Damage(_damage);
        }
    }
}