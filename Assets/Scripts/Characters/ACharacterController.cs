using UnityEngine;

public abstract class ACharacterController : MonoBehaviour
{
    [SerializeField] protected LayerMask _team;
    private ACharacterVitality _vitality;
    private ACharacterMovements _movements;

    public LayerMask Team => _team;

    public ACharacterVitality Vitality => _vitality;

    public ACharacterMovements Movements => _movements;

    private void Awake()
    {
        _vitality ??= GetComponent<ACharacterVitality>();
        _movements ??= GetComponent<ACharacterMovements>();
    }
}