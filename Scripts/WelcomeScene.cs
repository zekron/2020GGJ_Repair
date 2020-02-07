using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WelcomeScene : MonoBehaviour
{
    public TextMesh _VerMessage;
    public Text _BGMVolume;
    public Text _FXVolume;
    public SpriteRenderer _Background;
    public SpriteRenderer _Title;
    public GameObject _Setting;
    public static bool _InWelcome = true;

    private void Awake()
    {
        _VerMessage.text = PrintPackageMessage();
    }
    void Start()
    {
        TouchMgr.instance.AddListener(TouchMgr.instance._StartBtn, StartGame);
        TouchMgr.instance.AddListener(TouchMgr.instance._ExitBtn, StartGame);

        SoundMgr.instance.PlayBGM(SoundMgr.instance._BGM._MainBGM);
    }

    #region Button Events
    void StartGame()
    {
        _Background.DOFade(0, 1).OnComplete(() => _Background.enabled = false);
        _Title.DOFade(0, 1).OnComplete(() => _Title.enabled = false);

        transform.DOLocalMoveY(50f, 2);
        RemoveWelcomeListener();

        _InWelcome = false;

        TouchMgr.instance.AddListener(TouchMgr.instance._EnterSettingBtn, EnterSetting);
    }

    void QuitGame()
    {
        Application.Quit();
        RemoveWelcomeListener();
    }
    void EnterSetting()
    {
        _Setting.SetActive(true);

        TouchMgr.instance.AddListener(TouchMgr.instance._ExitSettingBtn, ExitSetting);
        TouchMgr.instance._BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        TouchMgr.instance._FXSlider.onValueChanged.AddListener(SetFXVolume);
        TouchMgr.instance._BGMToggle.onValueChanged.AddListener(SetBGMMute);
        TouchMgr.instance._FXToggle.onValueChanged.AddListener(SetFXMute);

        TouchMgr.instance.RemoveListener(TouchMgr.instance._EnterSettingBtn, EnterSetting);

        TouchMgr.instance._BGMSlider.value = SoundMgr.instance.GetBGMVolume();
        TouchMgr.instance._FXSlider.value = SoundMgr.instance.GetFXVolume();
        _BGMVolume.text = SoundMgr.instance.GetBGMVolume().ToString();
        _FXVolume.text = SoundMgr.instance.GetFXVolume().ToString();
    }

    void ExitSetting()
    {
        _Setting.SetActive(false);

        TouchMgr.instance.AddListener(TouchMgr.instance._EnterSettingBtn, EnterSetting);

        TouchMgr.instance.RemoveListener(TouchMgr.instance._ExitSettingBtn, ExitSetting);
        TouchMgr.instance._BGMSlider.onValueChanged.RemoveListener(SetBGMVolume);
        TouchMgr.instance._FXSlider.onValueChanged.RemoveListener(SetFXVolume);
        TouchMgr.instance._BGMToggle.onValueChanged.RemoveListener(SetBGMMute);
        TouchMgr.instance._FXToggle.onValueChanged.RemoveListener(SetFXMute);
    }
    void SetBGMVolume(float value)
    {
        SoundMgr.instance.SetBGMVolume(value / 100);
        _BGMVolume.text = SoundMgr.instance.GetBGMVolume().ToString();
    }
    void SetBGMMute(bool value)
    {
        SoundMgr.instance.SetBGMMute(value);
    }
    void SetFXVolume(float value)
    {
        SoundMgr.instance.SetBGMVolume(value / 100);
        _FXVolume.text = SoundMgr.instance.GetFXVolume().ToString();
    }
    void SetFXMute(bool value)
    {
        SoundMgr.instance.SetFXMute(value);
    }
    #endregion

    void RemoveWelcomeListener()
    {
        TouchMgr.instance.RemoveListener(TouchMgr.instance._StartBtn, StartGame);
        TouchMgr.instance.RemoveListener(TouchMgr.instance._ExitBtn, StartGame);
    }

    string PrintPackageMessage()
    {
        return string.Format("{0} - {1} - {2}", StaticData.PackageName, StaticData.PackageVer, StaticData.PackageTime);
    }
}
