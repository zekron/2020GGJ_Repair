using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WelcomeMgr : MonoBehaviour
{
    public static WelcomeMgr instance = null;

    public SpriteRenderer _Background;
    public SpriteRenderer _Title;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SoundMgr.instance.PlayBGM(SoundMgr.instance._BGM._MainBGM);
    }

    public void AddListener()
    {
        TouchMgr.instance.AddListener(TouchMgr.instance._StartBtn, StartGame);
        TouchMgr.instance.AddListener(TouchMgr.instance._ExitBtn, QuitGame);
    }

    void RemoveWelcomeListener()
    {
        TouchMgr.instance.RemoveListener(TouchMgr.instance._StartBtn, StartGame);
        TouchMgr.instance.RemoveListener(TouchMgr.instance._ExitBtn, StartGame);
    }

    #region Button Events
    void StartGame()
    {
        _Background.DOFade(0, 1).OnComplete(() => _Background.enabled = false);
        _Title.DOFade(0, 1).OnComplete(() =>
        {
            _Title.enabled = false;
            gameObject.SetActive(false);
        });

        RemoveWelcomeListener();

        GameMgr.instance.GameState = eGameState.InGame;
    }

    void QuitGame()
    {
        RemoveWelcomeListener();
        Application.Quit();
    }
    #endregion
}
