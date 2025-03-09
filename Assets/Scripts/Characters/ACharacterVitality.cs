using System;
using UnityEngine;

public abstract class ACharacterVitality : MonoBehaviour
{
    [SerializeField] protected int _life = 1;
    protected ACharacterController _characterController;

    public int Life
    {
        get => _life;
        set => _life = value;
    }

    public Action<ACharacterController> OnKilled;
    public Action<ACharacterController, int> OnDamaged;

    protected virtual void Awake()
    {
        _characterController ??= GetComponent<ACharacterController>();
    }

    public void Damage(int damage)
    {
        _life -= damage;
        OnDamaged?.Invoke(_characterController, damage);
        
        if (_life <= 0)
        {
            OnKilled?.Invoke(_characterController);
            gameObject.SetActive(false);
        }
    }
}