using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : ACharacterMovements
{
    [SerializeField] private LayerMask _movementLayer;
    private NavMeshAgent _agent;
    private PlayerController _controller;
    private Vector3 _cameraPos;

    protected override void Awake()
    {
        base.Awake();

        _controller ??= GetComponent<PlayerController>();
        _agent ??= GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _cameraPos = _controller.Camera.transform.position - _controller.transform.position;
    }

    private void Update()
    {
        if (_controller.LeftClick)
        {
            Ray ray = _controller.Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _movementLayer))
                _agent.SetDestination(hit.point);
        }

        Correction();
        _controller.Camera.transform.position = _controller.transform.position + _cameraPos;
    }
    
    protected void Correction()
    {
        var position1 = transform.position;
        position1.y = 1f;
        transform.position = position1;
        _rb.velocity = Vector3.zero;
    }
}