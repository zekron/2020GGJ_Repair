﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIWelcomeMgr : MonoBehaviour
{
    public static UIWelcomeMgr instance = null;

    public GameObject _Layer;
    public SpriteRenderer _Background;
    public SpriteRenderer _Title;
    public SpriteRenderer _Start, _Exit;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        EnterWelcomeDialog();
        AddListener();
    }

    void AddListener()
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
        _Layer.SetActive(true);
        _Start.DOFade(1, 0).OnStart(() => _Start.enabled = true);
        _Exit.DOFade(1, 0).OnStart(() => _Exit.enabled = true);
        _Background.DOFade(1, 0).OnStart(() => _Background.enabled = true);
        _Title.DOFade(1, 0).OnStart(() => _Title.enabled = true);
        TouchMgr.instance._StartBtn.targetGraphic.DOColor(Color.white, 0);
        TouchMgr.instance._ExitBtn.targetGraphic.DOColor(Color.white, 0);

        if (!SoundMgr.instance.IsPlaying(0, 0))
            SoundMgr.instance.PlayBGM(SoundMgr.instance._BGM._MainBGM, 0);
    }
    void StartGame()
    {
        _Start.DOFade(0, 1).OnComplete(() => _Start.enabled = false);
        _Exit.DOFade(0, 1).OnComplete(() => _Exit.enabled = false);
        _Background.DOFade(0, 1).OnComplete(() => _Background.enabled = false);
        _Title.DOFade(0, 1).OnComplete(() =>
        {
            _Title.enabled = false;
            _Layer.SetActive(false);
        });

        RemoveWelcomeListener();

            GameMgr.instance.GameState = eGameState.eInGame;
    }

    void QuitGame()
    {
        RemoveWelcomeListener();
        Application.Quit();
    }
    #endregion
}