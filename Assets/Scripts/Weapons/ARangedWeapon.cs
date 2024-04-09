using System;
using UnityEngine;

public abstract class ARangedWeapon : AWeapon
{
    [SerializeField] private float _rate;
    [SerializeField] protected RangedDamageData _data;
    [SerializeField] private PoolingManager.PoolableObject _bullet;

    protected virtual void Start()
    {
        InvokeRepeating(nameof(Fire), 1f / _rate, 1f / _rate);
    }

    protected virtual void Fire()
    {
        ACharacterController nearest = TestSpawner.Instance.GetNearestFromPlayer();

        if (ReferenceEquals(nearest, null))
            return;

        GameObject spawned = PoolingManager.Instance.Spawn(ref _bullet, transform.position, Quaternion.identity);

        if (spawned.TryGetComponent<ABullet>(out ABullet bullet))
        {
            Vector3 direction = nearest.transform.position - transform.position;
            direction.Normalize();
            bullet.Data = RangedDamageData.create(_data, _controller, direction, _targetTeam);
        }
    }

    [Serializable]
    public struct RangedDamageData
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private int _piercing;
        private LayerMask _targetTeam;
        private ACharacterController _owner;
        private Vector3 _direction;

        public static RangedDamageData create(RangedDamageData original, ACharacterController owner, Vector3 direction, LayerMask targetTeam)
        {
            var clone = new RangedDamageData();
            clone._targetTeam = targetTeam;
            clone._damage = original._damage;
            clone._speed = original._speed;
            clone._piercing = original._piercing;
            clone._owner = owner;
            clone._direction = direction;
            return clone;
        }

        public LayerMask TargetTeam => _targetTeam;

        public int Damage => _damage;

        public float Speed => _speed;

        public int Piercing
        {
            get => _piercing;
            set => _piercing = value;
        }

        public ACharacterController Owner => _owner;

        public Vector3 Direction => _direction;
    }
}