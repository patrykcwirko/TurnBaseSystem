using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler instance;

    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public static ObjectPooler Instance 
    { 
        get 
        {
            if (instance == null)
            {
                CreateInstance();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetObject(GameObject _gameObject)
    {
        if (objectPool.TryGetValue(_gameObject.name, out Queue<GameObject> _objectList))
        {
            if (_objectList.Count == 0)
                return CreateNewObject(_gameObject);
            else
            {
                GameObject _object = _objectList.Dequeue();
                _object.SetActive(true);
                return _object;
            }
        }
        else
            return CreateNewObject(_gameObject);
    }

    public void ReturnGameObject(GameObject _gameObject)
    {
        if (objectPool.TryGetValue(_gameObject.name, out Queue<GameObject> _objectList))
        {
            _objectList.Enqueue(_gameObject);
        }
        else
        {
            Queue<GameObject> _newObjectQueue = new Queue<GameObject>();
            _newObjectQueue.Enqueue(_gameObject);
            objectPool.Add(_gameObject.name, _newObjectQueue);
        }
        _gameObject.SetActive(false);
    }

    public int GetPoolSize(GameObject _gameObject)
    {
        if (objectPool.TryGetValue(_gameObject.name, out Queue<GameObject> _objectList))
        {
            return _objectList.Count;
        }
        else return 0;
    }

    private static void CreateInstance()
    {
        GameObject _newInstance = new GameObject();
        _newInstance.name = "ObjectPooler";
        _newInstance.AddComponent<ObjectPooler>();
        instance = _newInstance.GetComponent<ObjectPooler>();
    }

    private GameObject CreateNewObject(GameObject _gameObject)
    {
        GameObject _newObject = Instantiate(_gameObject);
        _newObject.name = _gameObject.name;
        _newObject.transform.SetParent(transform);
        return _newObject;
    }

    private void CreateObjects(GameObject _gameObject, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            GameObject newGameObject = CreateNewObject(_gameObject);
            ReturnGameObject(newGameObject);
        }
    }
}
