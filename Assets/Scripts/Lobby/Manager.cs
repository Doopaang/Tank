using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    
    [HideInInspector]
    public LobbyPlayer player = null;

    void Awake()
    {
        Instance = this;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 280, Screen.height - 70, 150, 40), "돌아가기"))
        {
            Destroy(NetworkManager.singleton.gameObject);
            if (player != null)
            {
                Destroy(player.gameObject);
            }
            SceneManager.LoadScene("MainMenu");
        }
    }
}
