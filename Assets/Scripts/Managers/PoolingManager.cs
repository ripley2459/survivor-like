using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : AManager<PoolingManager>
{
    private readonly Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();

    /// <summary>
    /// Gives the opportunity to make operations of the spawned object.
    /// <param name="spawned">The spawned object.</param>
    /// </summary>
    public delegate void OnSpawn(GameObject spawned);

    /// <summary>
    /// Spawn the original.GameObject with the provided position and rotation.
    /// If a previously spawned object is deactivated in the hierarchy it will be used, otherwise a new will be instantiated.
    /// </summary>
    /// <param name="original">The PoolableObject to instantiate.</param>
    /// <param name="position">The position.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="onPreSpawn">Consumer called BEFORE the activation process. At this point the spawned object IS NOT ACTIVATED in the hierarchy.</param>
    /// <param name="onPostSpawn">Consumer called AFTER the activation process. At this point the spawned object IS ACTIVATED in the hierarchy and correctly placed.</param>
    /// <returns>The spawned game object. Can be a totally new or a previously used.</returns>
    public GameObject Spawn(ref PoolableObject original, Vector3 position, Quaternion rotation, OnSpawn onPreSpawn = null, OnSpawn onPostSpawn = null)
    {
        GameObject spawned = null;

        if (_pool.ContainsKey(original.GUID))
        {
            foreach (var mono in _pool[original.GUID])
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    spawned = mono;
                    break;
                }
            }
        }

        if (ReferenceEquals(spawned, null))
        {
            spawned = Instantiate(original.GameObject, position, rotation, transform);

            if (_pool.ContainsKey(original.GUID))
                _pool[original.GUID].Add(spawned);
            else _pool.Add(original.GUID, new List<GameObject> { spawned });
        }

        onPreSpawn?.Invoke(spawned);
        spawned.transform.position = position;
        spawned.transform.rotation = rotation;
        spawned.gameObject.SetActive(true);
        onPostSpawn?.Invoke(spawned);

        return spawned;
    }

    [Serializable]
    public struct PoolableObject
    {
        [SerializeField] private string _uuid;
        [SerializeField] public GameObject _gameObject;

        /// <summary>
        /// The game object used in the instantiate process.
        /// </summary>
        public GameObject GameObject => _gameObject;

        /// <summary>
        /// The GUID of the pool. Created the first time the pool is used.
        /// </summary>
        public string GUID
        {
            get
            {
                if (_uuid == null)
                    _uuid = System.Guid.NewGuid().ToString();
                return _uuid;
            }
        }
    }
}