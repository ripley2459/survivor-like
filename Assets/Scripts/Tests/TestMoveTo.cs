using System;
using UnityEngine;

public class TestMoveTo : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _destination;

    private Rigidbody _rb;

    private void OnEnable()
    {
        _rb ??= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var transform1 = transform;
        var position1 = transform1.position;
        Vector3 delta = _destination.position - position1;
        delta.Normalize();
        position1 += delta * Time.deltaTime * _moveSpeed;
        position1.y = 0.5f;
        transform1.position = position1;
        _rb.velocity = Vector3.zero; // Used to prevent cubes to struggle
    }
}