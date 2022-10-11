using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkBehaviour
{
    void Start()
    {
        LobbyManager.Instance.ResetSelect();

        if (isLocalPlayer)
        {
            Manager.Instance.player = this;
        }
    }

    void OnGUI()
    {
        if (isServer && isLocalPlayer)
        {
            if (GameManager.Instance != null &&
                GameManager.Instance.isLoaded)
            {
                return;
            }

            GUI.Box(new Rect(100, 360, 150, 120), "맵 선택");

            if (GUI.Button(new Rect(110, 390, 130, 20), "사막"))
            {
                LobbyManager.Instance.RpcSelect(LobbyManager.Terrain.Desert);
            }

            if (GUI.Button(new Rect(110, 420, 130, 20), "들판"))
            {
                LobbyManager.Instance.RpcSelect(LobbyManager.Terrain.Grass);
            }

            if (GUI.Button(new Rect(110, 450, 130, 20), "모래"))
            {
                LobbyManager.Instance.RpcSelect(LobbyManager.Terrain.Sand);
            }
        }
    }

    void OnDestroy()
    {
        Manager.Instance.player = null;
    }
}
