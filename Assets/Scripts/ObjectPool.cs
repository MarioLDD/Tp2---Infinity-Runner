using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

   [SerializeField] private List<Pool> pools;
    private Dictionary<int, Pool> poolDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        poolDictionary = new Dictionary<int, Pool>();
        for (int i = 0; i < pools.Count; i++)
        {
            pools[i].Initialize(i, transform);
            poolDictionary.Add(i, pools[i]);
        }
    }

    public GameObject GetObject(int elementID, Vector3 position)
    {
        if (poolDictionary.ContainsKey(elementID))
        {
            return poolDictionary[elementID].GetObject(position);
        }
        else
        {
            Debug.LogWarning($"No pool found with ID: {elementID}");
            return null;
        }
    }

    public void ReturnObject(int elementID, GameObject obj)
    {
        if (poolDictionary.ContainsKey(elementID))
        {
            poolDictionary[elementID].ReturnObject(obj);
        }
        else
        {
            Debug.LogWarning($"No pool found with ID: {elementID}");
        }
    }

    public int GetPoolCount()
    {
        return pools.Count;
    }

    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
        private Queue<GameObject> objects;
        private int elementID;

        public void Initialize(int id, Transform parent)
        {
            elementID = id;
            objects = new Queue<GameObject>();
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Object.Instantiate(prefab, parent);
                obj.SetActive(false);

                PooledObject pooledObject = obj.AddComponent<PooledObject>();
                pooledObject.SetPoolID(elementID, parent.GetComponent<ObjectPool>());

                objects.Enqueue(obj);
            }
        }

        public GameObject GetObject(Vector3 position)
        {
            GameObject obj;
            if (objects.Count > 0)
            {
                obj = objects.Dequeue();
            }
            else
            {
                obj = Object.Instantiate(prefab);

                PooledObject pooledObject = obj.AddComponent<PooledObject>();
                pooledObject.SetPoolID(elementID, GameObject.FindObjectOfType<ObjectPool>());
            }
            obj.transform.position = position;
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            objects.Enqueue(obj);
        }
    }
}