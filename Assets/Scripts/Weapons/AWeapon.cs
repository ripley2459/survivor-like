using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    [SerializeField] protected ETarget _targetEntity;
    [SerializeField] protected LayerMask _targetTeam;
    protected ACharacterController _controller;

    private void Awake()
    {
        _controller ??= GetComponentInParent<ACharacterController>();
    }

    public enum ETarget
    {
        Nearest
    }
}