using DG.Tweening;
using UnityEngine;

public class UISettingMgr : MonoBehaviour
{
    private void Awake()
    {
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    #region ButtonEvent
    public void EnterSetting()
    {
        TouchMgr.instance.AddListener(TouchMgr.instance._ExitSettingBtn, ExitToGame);
        TouchMgr.instance.AddListener(TouchMgr.instance._ExitGameBtn, ExitToMenu);

        TouchMgr.instance._BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        TouchMgr.instance._FXSlider.onValueChanged.AddListener(SetFXVolume);
        TouchMgr.instance._BGMToggle.onValueChanged.AddListener(SetBGMMute);
        TouchMgr.instance._FXToggle.onValueChanged.AddListener(SetFXMute);

        TouchMgr.instance._BGMSlider.value = SoundMgr.instance.GetBGMVolume();
        TouchMgr.instance._FXSlider.value = SoundMgr.instance.GetFXVolume();
    }

    void ExitToGame()
    {
        //transform.DOScale(0, 1)
        //    .OnComplete(() => GameMgr.instance.SetGameState(eGameState.eInGameplay));
        GameMgr.Instance.GetGameStateMgr().SetGameState(GameState.InGameplay);

        RemoveSettingListener();
    }

    void ExitToMenu()
    {
        transform.DOScale(0, 1)
            .OnComplete(() =>
            {
                GameMgr.Instance.GetGameStateMgr().SetGameState(GameState.InWelcome);
                //GameMgr.Instance.GameProtagonist.RebirthCharacter();
                CharacterPackage.instance.ClearItems();
            });

        RemoveSettingListener();
    }

    private void RemoveSettingListener()
    {
        TouchMgr.instance.RemoveListener(TouchMgr.instance._ExitSettingBtn, ExitToGame);
        TouchMgr.instance.RemoveListener(TouchMgr.instance._ExitGameBtn, ExitToMenu);

        TouchMgr.instance._BGMSlider.onValueChanged.RemoveListener(SetBGMVolume);
        TouchMgr.instance._FXSlider.onValueChanged.RemoveListener(SetFXVolume);
        TouchMgr.instance._BGMToggle.onValueChanged.RemoveListener(SetBGMMute);
        TouchMgr.instance._FXToggle.onValueChanged.RemoveListener(SetFXMute);
    }
    void SetBGMVolume(float value)
    {
        SoundMgr.instance.SetBGMVolume(value / 100);
    }
    void SetBGMMute(bool value)
    {
        SoundMgr.instance.SetBGMMute(value);
    }
    void SetFXVolume(float value)
    {
        SoundMgr.instance.SetBGMVolume(value / 100);
    }
    void SetFXMute(bool value)
    {
        SoundMgr.instance.SetFXMute(value);
    }
    #endregion
}
