//using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIInGameMgr _gameplayManager;
    [SerializeField] private UISettingMgr _settingManager;
    [SerializeField] private UIWelcomeMgr _welcomeManager;

    //const int INVISIBLE_UI_LAYER = 10;
    //const int VISIBLE_UI_LAYER = 5;
    static Vector3 INVISIBLE_UI_SCALE = Vector3.zero;
    static Vector3 VISIBLE_UI_SCALE = Vector3.one;

    void OnEnable()
    {
        //VISIBLE_UI_SCALE = transform.position;
    }
    public void OpenUIWelcome()
    {
        //_welcomeManager.transform.DOComplete(true);
        _welcomeManager.EnterWelcomeDialog();
        //_welcomeManager.gameObject.layer = VISIBLE_UI_LAYER;
        _welcomeManager.transform.localScale = VISIBLE_UI_SCALE;
        //_welcomeManager.transform.DOScale(1, 1);
    }
    public void CloseUIWelcome()
    {
        //_welcomeManager.transform.DOScale(0, 1)
        //    .OnComplete(() => _welcomeManager.gameObject.layer = INVISIBLE_UI_LAYER);
        _welcomeManager.transform.localScale = INVISIBLE_UI_SCALE;
        //_welcomeManager.ClosePanel(()=> _welcomeManager.gameObject.SetActive(false));
    }
    public void OpenUIGameplay()
    {
        //_gameplayManager.transform.DOComplete(true);
        _gameplayManager.AddGameplayListener();
        //_gameplayManager.gameObject.layer = VISIBLE_UI_LAYER;
        //_gameplayManager.transform.DOScale(1, 1);
        _gameplayManager.transform.localScale = VISIBLE_UI_SCALE;
    }
    public void CloseUIGameplay()
    {
        //_gameplayManager.transform.DOScale(0, 1)
        //    .OnComplete(() => _gameplayManager.gameObject.layer = INVISIBLE_UI_LAYER);
        _gameplayManager.transform.localScale = INVISIBLE_UI_SCALE;
    }
    public void OpenUISetting()
    {
        _settingManager.transform.localScale = VISIBLE_UI_SCALE;
        //_settingManager.gameObject.layer = VISIBLE_UI_LAYER;
        _settingManager.EnterSetting();
        //_settingManager.transform.DOScale(1, 1);
    }
    public void CloseUISetting()
    {
        //_settingManager.transform.DOScale(0, 1)
        //    .OnComplete(() => _settingManager.gameObject.SetActive(false));
        //_settingManager.gameObject.layer = INVISIBLE_UI_LAYER;
        _settingManager.transform.localScale = INVISIBLE_UI_SCALE;
    }
}
