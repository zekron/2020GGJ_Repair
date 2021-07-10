using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIWelcomeMgr : MonoBehaviour
{
    public static UIWelcomeMgr instance = null;

    [SerializeField] private Image _imageBackground, _imageTitle, _imageStart, _imageExit;

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
        _imageStart.DOFade(1, 0).OnStart(() => _imageStart.enabled = true);
        _imageExit.DOFade(1, 0).OnStart(() => _imageExit.enabled = true);
        _imageBackground.DOFade(1, 0).OnStart(() => _imageBackground.enabled = true);
        _imageTitle.DOFade(1, 0).OnStart(() => _imageTitle.enabled = true);

        if (!SoundMgr.instance.IsPlaying(0, 0))
            SoundMgr.instance.PlayBGM(SoundMgr.instance._BGM._MainBGM, 0);
    }
    void StartGame()
    {
        RemoveWelcomeListener();

        _imageStart.DOFade(0, 1).OnComplete(() => _imageStart.enabled = false);
        _imageExit.DOFade(0, 1).OnComplete(() => _imageExit.enabled = false);
        _imageBackground.DOFade(0, 1).OnComplete(() => _imageBackground.enabled = false);
        _imageTitle.DOFade(0, 1).OnComplete(() =>
        {
            _imageTitle.enabled = false;
            GameMgr.Instance.GetGameStateMgr().SetGameState(GameState.InGameplay);
        });
    }

    void QuitGame()
    {
        RemoveWelcomeListener();
        Application.Quit();
    }
    #endregion
}
