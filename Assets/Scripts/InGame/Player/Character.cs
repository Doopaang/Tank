using UnityEngine;
using UnityEngine.Networking;

public class Character : NetworkBehaviour
{
    [SerializeField]
    private GameObject boom;

    [SyncVar]
    public float HP = 10.0f;
    [HideInInspector]
    public float MaxHP;

    void Start()
    {
        MaxHP = HP;
    }

    public void Hit(int damage)
    {
        if (!isServer)
        {
            return;
        }

        HP -= damage;

        RpcSetHP();
    }

    private void Dead()
    {
        NetworkServer.Destroy(gameObject);

        CmdBoomEffect();

        if (isLocalPlayer)
        {
            GameManager.Instance.isDead = true;
            Camera.main.SendMessage("Dead");
        }
    }

    [ClientRpc]
    void RpcSetHP()
    {
        if (isLocalPlayer)
        {
            UIManager.Instance.SetHP(HP, MaxHP);
        }

        if (HP <= 0)
        {
            Dead();
        }
    }
    
    [Command]
    private void CmdBoomEffect()
    {
        GameObject obj = Instantiate(boom, transform.position, Camera.main.transform.rotation, EffectManager.Instance.transform);
        obj.transform.localScale = Vector3.one * 2.0f;
        NetworkServer.Spawn(boom);
    }
}
