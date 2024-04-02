using System.Collections;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PoolingManager.PoolableObject _original;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private int _spawnAmount = 1;
    [SerializeField] private LayerMask _spawnable;
    [SerializeField] private float _distance = 10f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// 200 FPS when spawning 50 each 0.5sec
    /// </summary>
    private IEnumerator Spawn()
    {
        while (true)
        {
            if (_spawnRate <= 0f)
                _spawnRate = 0.1f;

            yield return new WaitForSeconds(_spawnRate);

            Vector3 pos = Vector3.zero;

            for (int i = 0; i < _spawnAmount; i++)
            {
                pos = GetPosOutsideFrustum();
                pos.y = 0.5f;
                PoolingManager.Instance.Spawn(ref _original, pos, Quaternion.identity);
            }

            yield return null;
        }
    }

    private Vector3 GetPosOutsideFrustum()
    {
        int x, y;
        Ray ray;
        int side = Random.Range(0, 4);

        switch (side)
        {
            default:
                x = Random.Range(0, Screen.width);
                ray = _camera.ScreenPointToRay(new Vector3(x, -_distance, 0));
                break;
            case 1:
                y = Random.Range(0, Screen.height);
                ray = _camera.ScreenPointToRay(new Vector3(Screen.width + _distance, y, 0));
                break;
            case 2:
                x = Random.Range(0, Screen.width);
                ray = _camera.ScreenPointToRay(new Vector3(x, Screen.height + _distance, 0));
                break;
            case 3:
                y = Random.Range(0, Screen.height);
                ray = _camera.ScreenPointToRay(new Vector3(-_distance, y, 0));
                break;
        }

        return Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _spawnable) ? hit.point : GetPosOutsideFrustum();
    }
}