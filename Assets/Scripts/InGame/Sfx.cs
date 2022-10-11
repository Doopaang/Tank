using UnityEngine;
using UnityEngine.Networking;

public class Sfx : NetworkBehaviour
{
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Active(Vector3 position)
    {
        transform.position = position;
        source.Play();
        Invoke("DestroySfx", source.clip.length);
    }

    private void DestroySfx()
    {
        SpawnManager.Instance.UnSpawnObject(gameObject);
        NetworkServer.UnSpawn(gameObject);
        gameObject.SetActive(false);
    }
}
