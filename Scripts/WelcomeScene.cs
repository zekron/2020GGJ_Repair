using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WelcomeScene : MonoBehaviour
{
    public MyButton _StartBtn;
    public MyButton _ExitBtn;

    public TextMesh _VerMessage;
    public SpriteRenderer _Background;
    public SpriteRenderer _Title;
    public static bool _InWelcome = true;

    private void Awake()
    {
        _VerMessage.text = PrintPackageMessage();
    }
    void Start()
    {
        _StartBtn.AddListener(StartGame);
        _ExitBtn.AddListener(QuitGame);

        SoundMgr.instance.PlayBGM(
            SoundMgr.instance.
            _BGM.
            _MainBGM);
    }

    void StartGame()
    {
        _Background.DOFade(0, 1).OnComplete(() => _Background.enabled = false);
        _Title.DOFade(0, 1).OnComplete(() => _Title.enabled = false);
        _StartBtn.sprite.DOFade(0, 1).OnComplete(() => _StartBtn.sprite.enabled = false);
        _ExitBtn.sprite.DOFade(0, 1).OnComplete(() => _ExitBtn.sprite.enabled = false);

        transform.DOLocalMoveY(30, 2);
        RemoveListener();

        _InWelcome = false;
    }

    void QuitGame()
    {
        Application.Quit();
        RemoveListener();
    }

    void RemoveListener()
    {
        _StartBtn.RemoveListener(StartGame);
        _ExitBtn.RemoveListener(QuitGame);
    }

    string PrintPackageMessage()
    {
        return string.Format("{0} - {1} - {2}", StaticData.PackageName, StaticData.PackageVer, StaticData.PackageTime);
    }
}
