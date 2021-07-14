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
        AddWelcomeListener();
    }
    private void OnDisable()
    {
        RemoveWelcomeListener();
    }

    void AddWelcomeListener()
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
        //    _imageStart.DOFade(1, 0);
        //    _imageExit.DOFade(1, 0);
        //    _imageBackground.DOFade(1, 0);
        //    _imageTitle.DOFade(1, 0);

        if (!SoundMgr.instance.IsPlaying(0, 0))
            SoundMgr.instance.PlayBGM(SoundMgr.instance._BGM._MainBGM, 0);
    }
    void StartGame()
    {
        RemoveWelcomeListener();

        GameMgr.Instance.GetGameStateMgr().SetGameState(GameState.InGameplay);

        _startGameEvent.RaiseEvent();
    }

    public void ClosePanel(System.Action onCompleted)
    {
        //_imageStart.DOFade(0, 1);
        //_imageExit.DOFade(0, 1);
        //_imageBackground.DOFade(0, 1);
        //_imageTitle.DOFade(0, 1).OnComplete(() =>
        //{
            onCompleted();
        //});
    }

    void QuitGame()
    {
        RemoveWelcomeListener();
        Application.Quit();
    }
    #endregion
}
