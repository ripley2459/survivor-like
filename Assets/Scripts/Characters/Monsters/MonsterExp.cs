using UnityEngine;

public class MonsterExp : MonoBehaviour
{
    [SerializeField] protected PoolingManager.PoolableObject _expCube;
    private ACharacterController _characterController;

    private void Awake()
    {
        _characterController ??= GetComponent<ACharacterController>();
    }

    private void Start()
    {
        _characterController.Vitality.OnKilled += SpawnExpCube;
    }

    private void SpawnExpCube(ACharacterController controller)
    {
        PoolingManager.Instance.Spawn(ref _expCube, transform.position, Quaternion.identity);
    }
}