using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private Transform canvas;
    [SerializeField]
    private GameObject option;
    
    public void GameStart()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Help()
    {
        SceneManager.LoadScene("Help", LoadSceneMode.Additive);
    }

    public void Option()
    {
        if (option.activeSelf)
        {
            option.SendMessage("Close");
        }
        else
        {
            option.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
