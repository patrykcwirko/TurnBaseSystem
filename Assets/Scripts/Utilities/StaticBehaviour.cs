using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticBehaviour<T> : MonoBehaviour where T : Component
{
    protected static T instance;
    public static bool HasInstance => instance != null;
    public static T TryGetInstance() => HasInstance ? instance : null;
    public static T Current => instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(T).Name + "-AutoCreated";
                    instance = _obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        AsigneInstance();
        DontDestroyOnLoad(gameObject);
    }

    public abstract void AsigneInstance();
}
