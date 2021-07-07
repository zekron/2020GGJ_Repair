using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public static GameMgr instance = null;

    #region Const
    public const int _DefaultWidth = 1920;
    private const int m_DefaultHeight = 1080;
    private const float m_CameraSize = 5.4f;
    #endregion

    public eGameState GameState { get => m_GameState; }

    public Camera _MainCamera;
    public Transform _MainTrans;
    public Text _VerMessage;

    [HideInInspector]
    public Vector3 _MainTransScale = Vector3.one;
    public static Vector3 _GameMgrScale = new Vector3(0.4f, 0.4f, 0.4f);

    private eGameState m_GameState = eGameState.eInWelcome;

    private void Awake()
    {
        instance = this;
        _GameMgrScale = transform.localScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        SetScale();
        AddListener();

        SetGameState(eGameState.eInWelcome);
        _VerMessage.text = PrintPackageMessage();
    }

    private void AddListener()
    {
        Add_OnGameStateChanged(InGame);
    }
    private void SetScale()
    {
        SetMainTransScale();
        SmoothFollow.instance.SetCameraOffset(_MainTransScale);
        DestroyDetector.instance.SetDestroyDetectorScale(_GameMgrScale);
        DemoScene.instance.SetCharacterConfigScale(_MainTransScale);
    }

    void SetMainTransScale()
    {
        Vector2 mainDisplayRendering = new Vector2(Display.main.renderingWidth, Display.main.renderingHeight);
        _MainTransScale = new Vector3(mainDisplayRendering.x / _DefaultWidth, mainDisplayRendering.y / m_DefaultHeight, 1);
        _MainCamera.orthographicSize = mainDisplayRendering.y * 0.5f * 0.01f;

        _MainTrans.localScale = _MainTransScale;
        _GameMgrScale = transform.localScale = new Vector3(
            transform.localScale.x * _MainTransScale.x,
            transform.localScale.y * _MainTransScale.y,
            transform.localScale.z * _MainTransScale.z
            );
    }

    string PrintPackageMessage()
    {
        return string.Format("{0} - {1} - {2}", StaticData.PackageName, StaticData.PackageVer, StaticData.PackageTime);
    }

    public void SetGameState(eGameState state)
    {
        m_GameState = state;
        _OnGameStateChanged.Invoke(m_GameState);
    }
    #region Event
    void InGame(eGameState state)
    {
        switch (state)
        {
            case eGameState.eInWelcome:
                UIController.Instance.OpenUIWelcome();
                UIController.Instance.CloseUISetting();
                UIController.Instance.CloseUIGameplay();
                break;
            case eGameState.eInGameplay:
                UIController.Instance.OpenUIGameplay();
                UIController.Instance.CloseUIWelcome();
                UIController.Instance.CloseUISetting();
                break;
            case eGameState.eInSetting:
                UIController.Instance.OpenUISetting();
                UIController.Instance.CloseUIWelcome();
                UIController.Instance.CloseUIGameplay();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Event Listener
    public static MyGameStateEvent _OnGameStateChanged = new MyGameStateEvent();

    public static void Remove_OnGameStateChanged(UnityAction<eGameState> action)
    {
        _OnGameStateChanged.RemoveListener(action);
    }
    public static void Add_OnGameStateChanged(UnityAction<eGameState> action)
    {
        Remove_OnGameStateChanged(action);
        _OnGameStateChanged.AddListener(action);
    }
    #endregion
}
