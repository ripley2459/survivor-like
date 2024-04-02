using UnityEngine;

public abstract class APersistentManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance { get; set; }

    public static T Instance
    {
        get
        {
            if (ReferenceEquals(_instance, null))
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (ReferenceEquals(_instance, null))
                {
                    GameObject manager = new GameObject();
                    _instance = manager.AddComponent<T>();
                    manager.name = string.Format("{0} (Manager)", typeof(T));
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (!ReferenceEquals(_instance, null) && !ReferenceEquals(_instance, this))
            Destroy(this);
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}