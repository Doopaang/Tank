using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpManager : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 0.31f;
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync("Help");
    }
}
