using UnityEngine;

public class TestPooler : MonoBehaviour
{
    [SerializeField] private PoolingManager.PoolableObject _testA;
    [SerializeField] private PoolingManager.PoolableObject _testB;
    [SerializeField] private PoolingManager.PoolableObject _testC;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, 1f);
    }

    private void Spawn()
    {
        PoolingManager.Instance.Spawn(ref _testA, Vector3.zero, Quaternion.identity, OnPreSpawn, OnPostSpawn);
        PoolingManager.Instance.Spawn(ref _testB, Vector3.zero, Quaternion.identity, OnPreSpawn, OnPostSpawn);
        PoolingManager.Instance.Spawn(ref _testC, Vector3.zero, Quaternion.identity, OnPreSpawn, OnPostSpawn);
    }

    private void OnPreSpawn(GameObject mono)
    {
        // print("Pre spawn: " + mono.name);
    }

    private void OnPostSpawn(GameObject mono)
    {
        // print("Post spawn: " + mono.name);
    }
}