using UnityEngine;
using UnityEngine.Networking;

public class FireBullet : NetworkBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform firePos;
    [SerializeField]
    private GameObject fireEffect;

    [SerializeField]
    private float delay = 3.0f;
    private float maxDelay;

    [SerializeField]
    private float effectTime = 0.1f;

    void Start()
    {
        maxDelay = delay;

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer||
            !GameManager.Instance.isLoaded)
        {
            return;
        }

        delayFire();

        if (UIManager.Instance.menu.activeSelf)
        {
            return;
        }

        if (Input.GetButtonDown("Fire") &&
            delay == maxDelay)
        {
            CmdFire();

            delay = 0.0f;
        }
    }

    private void delayFire()
    {
        delay += Time.deltaTime;

        if (delay > maxDelay)
        {
            delay = maxDelay;
        }

        UIManager.Instance.SetFire(delay, maxDelay);
    }
    
    [Command]
    public void CmdFire()
    {
        GameObject effect = Instantiate(fireEffect, firePos.position, firePos.rotation, EffectManager.Instance.transform);
        Destroy(effect, effectTime);
        NetworkServer.Spawn(effect);
        
        GameObject sfx = SfxFactory.Instance.PopAudio("Fire");
        sfx.SendMessage("Active", firePos.position);
        NetworkServer.Spawn(sfx);
        
        GameObject bullet = SpawnManager.Instance.GetFromPool();
        bullet.SendMessage("Active", firePos);
        NetworkServer.Spawn(bullet, SpawnManager.Instance.assetId);
        
        animator.SetTrigger("Fire");
    }
}
