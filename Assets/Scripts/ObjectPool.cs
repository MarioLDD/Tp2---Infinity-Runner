using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    public List<Pool> pools;
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
            pools[i].Initialize(i, transform);  // Inicializar cada pool
            poolDictionary.Add(i, pools[i]);    // Usar el índice como el ID del pool
        }
    }

    // Método para obtener un objeto del pool correspondiente al ID y moverlo a la posición dada
    public GameObject GetObject(int elementID, Vector3 position)
    {
        if (poolDictionary.ContainsKey(elementID))
        {
            return poolDictionary[elementID].GetObject(position);
        }
        else
        {
            Debug.LogWarning("No pool found with ID: " + elementID);
            return null;
        }
    }

    // Método para devolver un objeto al pool
    public void ReturnObject(int elementID, GameObject obj)
    {
        if (poolDictionary.ContainsKey(elementID))
        {
            poolDictionary[elementID].ReturnObject(obj);
        }
        else
        {
            Debug.LogWarning("No pool found with ID: " + elementID);
        }
    }

    // Método para obtener la cantidad de tipos de objetos en el pool
    public int GetPoolCount()
    {
        return pools.Count;
    }

    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;        // Prefab del objeto
        public int size;                 // Cantidad de objetos a generar inicialmente
        private Queue<GameObject> objects;
        private int elementID;           // ID del tipo de objeto (asignado en ObjectPool)

        public void Initialize(int id, Transform parent)
        {
            elementID = id;
            objects = new Queue<GameObject>();
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Object.Instantiate(prefab, parent);
                obj.SetActive(false);

                // Asignar el identificador del pool al objeto
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
                obj = Object.Instantiate(prefab); // Crear nuevo si no hay objetos disponibles

                // Asignar el identificador del pool al objeto
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

