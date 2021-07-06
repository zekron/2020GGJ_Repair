using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController _instance = null;
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
    public void OpenUISetting()
    {
        _settingManager.gameObject.SetActive(true);
    }
    public void CloseUISetting()
    {
        _settingManager.gameObject.SetActive(false);
    }
}
