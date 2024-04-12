using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : ACharacterMovements
{
    [SerializeField] private LayerMask _movementLayer;
    private NavMeshAgent _agent;

    protected override void Awake()
    {
        base.Awake();

        _agent ??= GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _movementLayer))
        {
            _agent.SetDestination(hit.point);
        }
    }
}