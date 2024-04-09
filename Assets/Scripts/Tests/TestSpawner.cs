using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestSpawner : AManager<TestSpawner>
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private PoolingManager.PoolableObject _original;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private int _spawnAmount = 1;
    [SerializeField] private LayerMask _spawnable;
    [SerializeField] private float _distance = 10f;
    private List<ACharacterController> _spawned = new List<ACharacterController>();
    private ACharacterVitality _originalVitality;

    protected override void Awake()
    {
        base.Awake();

        _originalVitality = _original.Original.GetComponent<ACharacterVitality>();
    }

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

            Vector3 pos = Vector3.zero;

            for (int i = 0; i < _spawnAmount; i++)
            {
                pos = GetPosOutsideFrustum();
                pos.y = 0.5f;

                print("Will spawn a " + _original.Original.name + " at position " + pos);

                GameObject spawned = PoolingManager.Instance.Spawn(ref _original, pos, Quaternion.identity);

                if (spawned.TryGetComponent<ACharacterController>(out ACharacterController controller))
                {
                    ((MonsterMovements)controller.Movements).SetTarget(_player);
                    controller.Vitality.Life = _originalVitality.Life;
                    controller.Vitality.OnKilled += Despawn;
                    _spawned.Add(controller);
                }

                print("Spawned a " + spawned.gameObject.name + " at position " + spawned.transform.position);
            }

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private void Despawn(ACharacterController controller)
    {
        _spawned.Remove(controller);
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

    public ACharacterController GetNearestFromPlayer()
    {
        return GetNearestFromPos(_player.transform.position);
    }

    public ACharacterController GetNearestFromPos(Vector3 position)
    {
        ACharacterController bestTarget = null;
        float closestDist = Mathf.Infinity;

        foreach (ACharacterController pTarget in _spawned)
        {
            if (pTarget != null && pTarget.gameObject.activeInHierarchy)
            {
                Vector3 directionToTarget = pTarget.transform.position - position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDist)
                {
                    closestDist = dSqrToTarget;
                    bestTarget = pTarget;
                }
            }
        }

        return bestTarget;
    }
}