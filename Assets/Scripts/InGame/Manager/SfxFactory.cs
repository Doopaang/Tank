using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SfxFactory : NetworkBehaviour
{
    public static SfxFactory Instance { get; private set; }

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private AudioClip fire;
    [SerializeField]
    private AudioClip boom;

    private List<GameObject> pool;

    public int defaultSize = 5;

    public NetworkHash128 assetId { get; set; }

    public delegate GameObject SpawnDelegate(Vector3 position, NetworkHash128 assetId);
    public delegate void UnSpawnDelegate(GameObject spawned);

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        assetId = prefab.GetComponent<NetworkIdentity>().assetId;
        pool = new List<GameObject>();
        for (int i = 0; i < defaultSize; ++i)
        {
            AddObject();
        }

        ClientScene.RegisterSpawnHandler(assetId, SpawnObject, UnSpawnObject);
    }

    public GameObject PopAudio(string name)
    {
        GameObject obj = GetFromPool();
        ClipFactory(obj, name);
        return obj;
    }

    private GameObject GetFromPool()
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
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        obj.SetActive(false);
        pool.Add(obj);

        return obj;
    }

    private void ClipFactory(GameObject obj, string name)
    {
        AudioSource source = obj.GetComponent<AudioSource>();
        switch (name)
        {
            case "Fire":
                source.clip = fire;
                break;

            case "Boom":
                source.clip = boom;
                break;
        }
    }

    public void Change()
    {
        if (isServer)
        {
            foreach (GameObject obj in pool)
            {
                obj.GetComponent<AudioSource>().volume = OptionManager.Instance.option.SFX / 10.0f;
            }
        }
    }
}
