using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    AsyncOperation async;

    void Start()
    {
        async = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }

    void Update()
    {
        if(async.isDone)
        {
            SceneManager.UnloadSceneAsync("Intro");
        }
    }
}
