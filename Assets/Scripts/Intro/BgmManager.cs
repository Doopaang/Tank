using UnityEngine;

public class BgmManager : MonoBehaviour {
    public static BgmManager Instance { get; private set; }

    private AudioSource source;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        source = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void Change()
    {
        source.volume = OptionManager.Instance.option.BGM / 10.0f;
    }
}
