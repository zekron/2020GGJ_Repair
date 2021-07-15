using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIInGameMgr _gameplayManager;
    [SerializeField] private UISettingMgr _settingManager;
    [SerializeField] private UIWelcomeMgr _welcomeManager;

    const int INVISIBLE_UI_LAYER = 10;
    const int VISIBLE_UI_LAYER = 5;
    public void OpenUIWelcome()
    {
        _welcomeManager.transform.DOComplete(true);
        _welcomeManager.AddWelcomeListener();
        _welcomeManager.gameObject.layer = VISIBLE_UI_LAYER;
        _welcomeManager.transform.DOScale(1, 1);
    }
    public void CloseUIWelcome()
    {
        _welcomeManager.transform.DOScale(0, 1)
            .OnComplete(() => _welcomeManager.gameObject.layer = INVISIBLE_UI_LAYER);
        //_welcomeManager.ClosePanel(()=> _welcomeManager.gameObject.SetActive(false));
    }
    public void OpenUIGameplay()
    {
        _gameplayManager.transform.DOComplete(true);
        _gameplayManager.AddGameplayListener();
        _gameplayManager.gameObject.layer = VISIBLE_UI_LAYER;
        _gameplayManager.transform.DOScale(1, 1);
    }
    public void CloseUIGameplay()
    {
        _gameplayManager.transform.DOScale(0, 1)
            .OnComplete(() => _gameplayManager.gameObject.layer = INVISIBLE_UI_LAYER);
    }
    public void OpenUISetting()
    {
        _settingManager.gameObject.layer = VISIBLE_UI_LAYER;
        _settingManager.EnterSetting();
        _settingManager.transform.DOScale(1, 1);
    }
    public void CloseUISetting()
    {
        //_settingManager.transform.DOScale(0, 1)
        //    .OnComplete(() => _settingManager.gameObject.SetActive(false));
        _settingManager.gameObject.layer = INVISIBLE_UI_LAYER;
    }
}
