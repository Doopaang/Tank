using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    private Rigidbody rigid;

    public int damage = 1;
    public float bulletSpeed = 1000.0f;
    public float bulletTime = 5.0f;

    [SerializeField]
    private GameObject boomEffect;
    public float boomLimit = 1500.0f;
    public float boomRadius = 1.0f;
    public float boomTime = 3.0f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        switch (obj.tag)
        {
            case "PlayerTop":
                obj.GetComponentInParent<Character>().Hit(damage);
                break;
        }

        if (collision.impulse.sqrMagnitude / Time.fixedDeltaTime > boomLimit)
        {
            CmdBoomEffect(collision.contacts[0].point);
            Boom(collision.contacts[0].point);
        }
    }

    public void Active(Transform transform)
    {
        this.transform.SetPositionAndRotation(transform.position, transform.rotation);
        rigid.AddForce(transform.forward * bulletSpeed);

        Invoke("DestroyBullet", bulletTime);
    }

    private void DestroyBullet()
    {
        rigid.velocity = Vector3.zero;
        SpawnManager.Instance.UnSpawnObject(gameObject);
        NetworkServer.UnSpawn(gameObject);
    }

    private void Boom(Vector3 position)
    {
        CancelInvoke();
        DestroyBullet();

        GameObject sfx = SfxFactory.Instance.PopAudio("Boom");
        sfx.SendMessage("Active", position);
        NetworkServer.Spawn(sfx);

        Collider[] colls = Physics.OverlapSphere(position, boomRadius);
        List<Character> objects = new List<Character>();
        foreach (Collider coll in colls)
        {
            if (coll.gameObject.tag == "Player" &&
                !objects.Contains(coll.gameObject.GetComponentInParent<Character>()))
            {
                objects.Add(coll.gameObject.GetComponentInParent<Character>());
                
                coll.GetComponentInParent<Character>().Hit(damage);
            }
        }
    }

    [Command]
    private void CmdBoomEffect(Vector3 position)
    {
        GameObject boom = Instantiate(boomEffect, position, Camera.main.transform.rotation, EffectManager.Instance.transform);
        NetworkServer.Spawn(boom);
    }
}
