using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController _instance = null;
    [SerializeField] private UIInGameMgr _gameplayManager;
    [SerializeField] private UISettingMgr _settingManager;
    [SerializeField] private UIWelcomeMgr _welcomeManager;
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIController>();
            }
            return _instance;
        }
    }

    public void OpenUIWelcome()
    {
        _welcomeManager.gameObject.SetActive(true);
    }
    public void CloseUIWelcome()
    {
        _welcomeManager.gameObject.SetActive(false);
    }
    public void OpenUIGameplay()
    {
        _gameplayManager.gameObject.SetActive(true);
        _gameplayManager.transform.DOScale(1, 1);
    }
    public void CloseUIGameplay()
    {
        _gameplayManager.transform.DOScale(0, 1)
            .OnComplete(() => _gameplayManager.gameObject.SetActive(false));
    }
    public void OpenUISetting()
    {
        _settingManager.gameObject.SetActive(true);
        _settingManager.transform.DOScale(1, 1);
    }
    public void CloseUISetting()
    {
        //_settingManager.transform.DOScale(0, 1)
        //    .OnComplete(() => _settingManager.gameObject.SetActive(false));
        _settingManager.gameObject.SetActive(false);
    }
}
