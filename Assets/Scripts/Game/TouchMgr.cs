using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchMgr : MonoBehaviour
{
    public static TouchMgr instance = null;

    public Button _StartBtn;
    public Button _ExitBtn;
    public Button _EnterSettingBtn;
    public Button _ExitSettingBtn;
    public Button _ExitGameBtn;

    public Button _Item01;
    public Button _Item02;
    public Button _Item03;
    public Button[] _Items;

    public Slider _BGMSlider;
    public Slider _FXSlider;
    public Toggle _BGMToggle;
    public Toggle _FXToggle;
    private void Awake()
    {
        instance = this;
    }

    public void AddListener(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
        //button.targetGraphic.enabled = true;
    }

    public void RemoveListener(Button button, UnityAction action)
    {
        button.onClick.RemoveListener(action);
        //button.targetGraphic.enabled = false;
    }
}
