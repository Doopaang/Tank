using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }
    [HideInInspector]
    public bool isLoaded = false;

    [SerializeField]
    private List<GameObject> terrains;

    [HideInInspector]
    public bool isDead = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        Instantiate(terrains[(int)LobbyManager.Instance.select]);
    }

    void Update()
    {
        if (!isLoaded)
        {
            isLoaded = SceneManager.GetSceneByName("UI").isLoaded;

            if (isLoaded)
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerTop");
                foreach (GameObject obj in objs)
                {
                    obj.GetComponent<Character>().Hit(0);
                }
            }
            return;
        }

        if (Input.GetButtonDown("Cancel") &&
            !UIManager.Instance.gameOver.activeSelf)
        {
            UIManager.Instance.TriggerMenu();
        }

        if (GameObject.FindGameObjectsWithTag("PlayerTop").Length <= 1)
        {
            if (isDead)
            {
                UIManager.Instance.GameOver("게임 오버");
            }
            else
            {
                UIManager.Instance.GameOver("승리");
            }
        }
    }
}
