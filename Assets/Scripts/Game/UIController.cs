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

    public UISettingMgr UISettingManager { get => _settingManager; }
    public UIWelcomeMgr UIWelcomeManager { get => _welcomeManager; }

    // Start is called before the first frame update
    void Start()
    {

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
