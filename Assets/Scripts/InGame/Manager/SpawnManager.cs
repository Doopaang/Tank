using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SpawnManager : NetworkBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField]
    private GameObject bullet;

    private List<GameObject> pool;

    public int defaultSize = 5;

    public NetworkHash128 assetId { get; set; }

    public delegate GameObject SpawnDelegate(Vector3 position, NetworkHash128 assetId);
    public delegate void UnSpawnDelegate(GameObject spawned);

    void Awake()
    {
        Instance = this;

        pool = new List<GameObject>();
    }

    void Start()
    {
        assetId = bullet.GetComponent<NetworkIdentity>().assetId;
        for (int i = 0; i < defaultSize; ++i)
        {
            AddObject();
        }

        ClientScene.RegisterSpawnHandler(assetId, SpawnObject, UnSpawnObject);
    }

    public GameObject GetFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = AddObject();
        newObj.SetActive(true);
        return newObj;
    }

    public GameObject SpawnObject(Vector3 position, NetworkHash128 assetId)
    {
        return GetFromPool();
    }

    public void UnSpawnObject(GameObject spawned)
    {
        spawned.SetActive(false);
    }

    private GameObject AddObject()
    {
        GameObject obj = Instantiate(bullet, Vector3.zero, Quaternion.identity, transform);
        obj.SetActive(false);
        pool.Add(obj);

        return obj;
    }
}