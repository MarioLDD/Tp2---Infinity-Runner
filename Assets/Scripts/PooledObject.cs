using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private int poolID;
    private ObjectPool objectPool;

    public void SetPoolID(int id, ObjectPool pool)
    {
        poolID = id;
        objectPool = pool;
    }

    public void Deactivate()
    {
        objectPool.ReturnObject(poolID, gameObject);
    }
}