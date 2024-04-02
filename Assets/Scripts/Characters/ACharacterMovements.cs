using UnityEngine;

public abstract class ACharacterMovements : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Rigidbody _rb;

    private void Awake()
    {
        _rb ??= GetComponent<Rigidbody>();
    }
}