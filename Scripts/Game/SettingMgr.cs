using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMgr : MonoBehaviour
{
    public static SettingMgr instance = null;

    public SpriteRenderer _SettingSprite;
    public Canvas _Setting;
    public Text _BGMVolume;
    public Text _FXVolume;

    private void Awake()
    {
        instance = this;
    }

    public void AddEnter_SettingListener()
    {
        TouchMgr.instance.AddListener(TouchMgr.instance._EnterSettingBtn, EnterSetting);
        TouchMgr.instance.RemoveListener(TouchMgr.instance._ExitSettingBtn, ExitSetting);
    }
    public void AddExit_SettingListener()
    {
        TouchMgr.instance.AddListener(TouchMgr.instance._ExitSettingBtn, ExitSetting);
        TouchMgr.instance.RemoveListener(TouchMgr.instance._EnterSettingBtn, EnterSetting);
    }

    #region ButtonEvent
    void EnterSetting()
    {
        if (!_SettingSprite.enabled)
        {
            _SettingSprite.transform.localScale = Vector3.zero;
            _SettingSprite.enabled = true;
            _SettingSprite.transform.DOScale(1, 1);
            return;
        }

        _SettingSprite.transform.DOScale(0, 1).OnComplete(() => _SettingSprite.enabled = false);
        GameMgr.instance.GameState = eGameState.InSetting;
        _Setting.enabled = true;

        TouchMgr.instance._BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        TouchMgr.instance._FXSlider.onValueChanged.AddListener(SetFXVolume);
        TouchMgr.instance._BGMToggle.onValueChanged.AddListener(SetBGMMute);
        TouchMgr.instance._FXToggle.onValueChanged.AddListener(SetFXMute);

        TouchMgr.instance._BGMSlider.value = SoundMgr.instance.GetBGMVolume();
        TouchMgr.instance._FXSlider.value = SoundMgr.instance.GetFXVolume();
        _BGMVolume.text = SoundMgr.instance.GetBGMVolume().ToString();
        _FXVolume.text = SoundMgr.instance.GetFXVolume().ToString();
    }

    void ExitSetting()
    {
        GameMgr.instance.GameState = eGameState.InGame;
        _Setting.enabled = false;

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
}
