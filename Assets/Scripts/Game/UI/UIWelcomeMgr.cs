using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIWelcomeMgr : MonoBehaviour
{
    public static UIWelcomeMgr instance = null;

    [SerializeField] private VoidEventChannelSO _startGameEvent;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        EnterWelcomeDialog();
    }

    public void AddWelcomeListener()
    {
        TouchMgr.instance.AddListener(TouchMgr.instance._StartBtn, StartGame);
        TouchMgr.instance.AddListener(TouchMgr.instance._ExitBtn, QuitGame);
    }

    void RemoveWelcomeListener()
    {
        TouchMgr.instance.RemoveListener(TouchMgr.instance._StartBtn, StartGame);
        TouchMgr.instance.RemoveListener(TouchMgr.instance._ExitBtn, QuitGame);
    }

    #region Button Events
    void EnterWelcomeDialog()
    {
        if (!SoundMgr.instance.IsPlaying(0, 0))
            SoundMgr.instance.PlayBGM(SoundMgr.instance._BGM._MainBGM, 0);
    }
    void StartGame()
    {
        RemoveWelcomeListener();

        GameMgr.Instance.GetGameStateMgr().SetGameState(GameState.InGameplay);

        _startGameEvent.RaiseEvent();
    }

    void QuitGame()
    {
        RemoveWelcomeListener();
        Application.Quit();
    }
    #endregion
}
