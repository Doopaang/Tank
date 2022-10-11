using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField]
    private Image HP;
    public float HPSpeed = 0.01f;

    [SerializeField]
    private Image fire;
    [SerializeField]
    private Image fireFull;

    public GameObject menu;
    public GameObject gameOver;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "UI";
        canvas.sortingOrder = 1;
        canvas.planeDistance = 0.31f;
    }

    void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetHP(float HP, float MaxHP)
    {
        StopCoroutine(AnimateHP(HP, MaxHP));
        StartCoroutine(AnimateHP(HP, MaxHP));
    }

    private IEnumerator AnimateHP(float HP, float MaxHP)
    {
        while (this.HP.fillAmount > HP / MaxHP)
        {
            this.HP.fillAmount -= HPSpeed;

            yield return new WaitForFixedUpdate();
        }
    }

    public void SetFire(float delay, float maxDelay)
    {
        fire.fillAmount = delay / maxDelay;
        fireFull.enabled = fire.fillAmount == 1.0f;
    }

    public void TriggerMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            OptionManager.Instance.Save();
            SfxFactory.Instance.Change();
        }
        else
        {
            menu.SetActive(true);
        }
        Cursor.visible = menu.activeSelf;
        Cursor.lockState = menu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void Dead()
    {
        menu.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameOver(string text)
    {
        menu.SetActive(false);
        gameOver.SetActive(true);
        gameOver.GetComponentInChildren<Text>().text = text;
        StartCoroutine(FadeIn());

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private IEnumerator FadeIn()
    {
        Image image = gameOver.GetComponent<Image>();
        while (image.color.a < 200.0f / 255.0f)
        {
            Color temp = image.color;
            temp.a += 5.0f / 255.0f;
            image.color = temp;

            yield return new WaitForFixedUpdate();
        }
    }
}
