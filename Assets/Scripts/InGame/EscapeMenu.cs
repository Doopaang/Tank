using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField]
    private Slider BGMSlider;
    [SerializeField]
    private InputField BGMInput;

    [SerializeField]
    private Slider SFXSlider;
    [SerializeField]
    private InputField SFXInput;

    [SerializeField]
    private Slider MouseSlider;
    [SerializeField]
    private InputField MouseInput;
    
    void Start()
    {
        BGMSlider.value = OptionManager.Instance.option.BGM;
        BGMInput.text = OptionManager.Instance.option.BGM.ToString();
        SFXSlider.value = OptionManager.Instance.option.SFX;
        SFXInput.text = OptionManager.Instance.option.SFX.ToString();
        MouseSlider.value = OptionManager.Instance.option.Sensitivity;
        MouseInput.text = OptionManager.Instance.option.Sensitivity.ToString();
    }
    
    public void BGMSlide()
    {
        BGMInput.text = BGMSlider.value.ToString();
        OptionManager.Instance.option.BGM = (int)BGMSlider.value;
    }

    public void BGMField()
    {
        int value = int.Parse(BGMInput.text);
        if (value < BGMSlider.minValue)
        {
            value = (int)BGMSlider.minValue;
            BGMInput.text = value.ToString();
        }
        if (value > BGMSlider.maxValue)
        {
            value = (int)BGMSlider.maxValue;
            BGMInput.text = value.ToString();
        }
        BGMSlider.value = value;
        OptionManager.Instance.option.BGM = value;
    }

    public void SFXSlide()
    {
        SFXInput.text = SFXSlider.value.ToString();
        OptionManager.Instance.option.SFX = (int)SFXSlider.value;
    }

    public void SFXField()
    {
        int value = int.Parse(SFXInput.text);
        if (value < SFXSlider.minValue)
        {
            value = (int)SFXSlider.minValue;
            SFXInput.text = value.ToString();
        }
        if (value > SFXSlider.maxValue)
        {
            value = (int)SFXSlider.maxValue;
            SFXInput.text = value.ToString();
        }
        SFXSlider.value = value;
        OptionManager.Instance.option.SFX = value;
    }

    public void SensitivitySlide()
    {
        MouseInput.text = MouseSlider.value.ToString();
        OptionManager.Instance.option.Sensitivity = MouseSlider.value;
    }

    public void SensitivityField()
    {
        float value = float.Parse(MouseInput.text);
        if (value < MouseSlider.minValue)
        {
            value = MouseSlider.minValue;
            MouseInput.text = value.ToString();
        }
        if (value > MouseSlider.maxValue)
        {
            value = MouseSlider.maxValue;
            MouseInput.text = value.ToString();
        }
        MouseSlider.value = value;
        OptionManager.Instance.option.Sensitivity = value;
    }
    
    public void ResetButton()
    {
        BGMSlider.value = OptionManager.Instance.bgmDefault;
        SFXSlider.value = OptionManager.Instance.sfxDefault;
        MouseSlider.value = OptionManager.Instance.mouseDefault;
    }
    
    public void Close()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.TriggerMenu();
        }
        else
        {
            gameObject.SetActive(false);
            OptionManager.Instance.Save();
            BgmManager.Instance.Change();
            if (SfxFactory.Instance)
            {
                SfxFactory.Instance.Change();
            }
        }
    }
}
