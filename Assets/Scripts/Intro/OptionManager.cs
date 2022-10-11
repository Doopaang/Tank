using UnityEngine;
using System.IO;

public class OptionManager : MonoBehaviour
{
    public static OptionManager Instance { get; private set; }

    public Option option;
    private string path;

    public float mouseDefault = 5.0f;
    public int bgmDefault = 10;
    public int sfxDefault = 10;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        path = Application.dataPath;

        Load();

        DontDestroyOnLoad(gameObject);
    }
    
    public void Load()
    {
        string str = File.ReadAllText(path + "/Option.json", System.Text.Encoding.UTF8);
        option = JsonUtility.FromJson<Option>(str);
    }

    public void Save()
    {
        string str = JsonUtility.ToJson(option, true);
        str.Replace("\n", "\r\n");
        File.WriteAllText(path + "/Option.json", str);
    }
}

[System.Serializable]
public class Option
{
    public int BGM;
    public int SFX;
    public float Sensitivity;
}