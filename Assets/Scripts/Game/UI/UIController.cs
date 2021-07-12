using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIInGameMgr _gameplayManager;
    [SerializeField] private UISettingMgr _settingManager;
    [SerializeField] private UIWelcomeMgr _welcomeManager;

    public void OpenUIWelcome()
    {
        _welcomeManager.gameObject.SetActive(true);
    }
    public void CloseUIWelcome()
    {
        _welcomeManager.ClosePanel(()=> _welcomeManager.gameObject.SetActive(false));
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
