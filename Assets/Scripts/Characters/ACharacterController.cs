using UnityEngine;

public abstract class ACharacterController : MonoBehaviour
{
    [SerializeField] protected LayerMask _team;

    public LayerMask Team => _team;
}