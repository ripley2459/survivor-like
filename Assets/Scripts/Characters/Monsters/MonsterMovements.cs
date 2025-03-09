using UnityEngine;

public class MonsterMovements : ACharacterMovements
{
    [SerializeField] protected Transform _destination;

    private void Awake()
    {
        _rb ??= GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform target)
    {
        _destination = target;
    }

    private void Update()
    {
        if (ReferenceEquals(_destination, null))
            return;

        var position1 = transform.position;
        Vector3 delta = _destination.position - position1;
        delta.Normalize();
        position1 += delta * Time.deltaTime * _speed;
        transform.position = position1;
        Correction();
    }
    
    protected void Correction()
    {
        var position1 = transform.position;
        position1.y = 0.5f;
        transform.position = position1;
        _rb.velocity = Vector3.zero;
    }
}