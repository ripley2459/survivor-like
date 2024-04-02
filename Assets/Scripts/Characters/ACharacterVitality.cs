using System;
using UnityEngine;

public abstract class ACharacterVitality : MonoBehaviour
{
    [SerializeField] protected int _life = 100;
    protected ACharacterController _characterController;

    public Action<ACharacterVitality> OnKilled;

    private void Awake()
    {
        _characterController ??= GetComponentInParent<ACharacterController>();
    }

    public void Damage(int damage)
    {
        _life -= damage;

        if (_life <= 0)
            OnKilled?.Invoke(this);
    }
}