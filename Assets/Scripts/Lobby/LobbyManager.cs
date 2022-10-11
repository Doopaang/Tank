using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> terrains;

    [HideInInspector]
    [SyncVar]
    public Terrain select = 0;

    public enum Terrain { Desert, Grass, Sand }

    void Awake()
    {
        Instance = this;
    }

    void OnGUI()
    {
        if (isServer)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 40;
            style.normal.textColor = Color.white;

            GUI.Label(new Rect(100, 122, 500, 40), "Adress: " + Network.player.ipAddress, style);
        }
    }

    [ClientRpc]
    public void RpcSelect(Terrain select)
    {
        terrains[(int)this.select].SetActive(false);
        this.select = select;
        terrains[(int)this.select].SetActive(true);
    }

    public void ResetSelect()
    {
        foreach (GameObject obj in terrains)
        {
            obj.SetActive(false);
        }
        terrains[(int)select].SetActive(true);
    }
}
