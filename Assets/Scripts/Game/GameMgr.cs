using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    private static GameMgr _instance = null;
    public static GameMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameMgr>();
            }
            return _instance;
        }
    }

    [Header("Attachment")]
    [SerializeField] private Camera _MainCamera;
    [SerializeField] private Transform _MainTrans;
    [SerializeField] private Text _VerMessage;

    [Header("Event")]
    [SerializeField] private GameStateEventChannelSO _gameStateEvent;
    [SerializeField] private VoidEventChannelSO _startNewGameEvent;

    [Header("Manager & Controller")]
    [SerializeField] private GameStateMgr _gameStateMgr;
    [SerializeField] private UIController _UIController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Protagonist _protagonist;

    public Protagonist GameProtagonist => _protagonist;
    private Vector3 _MainTransScale = Vector3.one;
    public static Vector3 _GameMgrScale = Vector3.one;

    private void Awake()
    {
        _GameMgrScale = transform.localScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    private void OnValidate()
    {
        SetPackageMessage();
    }

    private void Init()
    {
        AddListener();

        _gameStateMgr.SetGameState(GameState.InWelcome);
    }

    private void AddListener()
    {
        _gameStateEvent.OnEventRaised += InGame;
        _startNewGameEvent.OnEventRaised += InstantiateProtagonist;
    }

    private void InstantiateProtagonist()
    {
        //avoid double instantiate
        if (_protagonist.isActiveAndEnabled) return;

        Protagonist temp = Instantiate<Protagonist>(_protagonist);
        temp.Init(_cameraController, StaticData.DefaultRebirthVector);
        _protagonist = temp;

        SetScale();
    }

    private void SetScale()
    {
        _protagonist.CamController.SetCameraOffset(_MainTransScale);
        //DestroyDetector.instance.SetDestroyDetectorScale(_GameMgrScale);
        //MoveController.instance.SetCharacterConfigScale(_MainTransScale);
    }

    void SetMainTransScale()
    {
        Vector2 mainDisplayRendering = new Vector2(Display.main.renderingWidth, Display.main.renderingHeight);
        _MainTransScale = new Vector3(
            mainDisplayRendering.x / StaticData.DEFAULT_WIDTH,
            mainDisplayRendering.y / StaticData.DEFAULT_HEIGHT,
            1);
        _MainCamera.orthographicSize = mainDisplayRendering.y * 0.5f * 0.01f;

        _MainTrans.localScale = _MainTransScale;
        _GameMgrScale = transform.localScale = new Vector3(
            transform.localScale.x * _MainTransScale.x,
            transform.localScale.y * _MainTransScale.y,
            transform.localScale.z * _MainTransScale.z
            );
    }

    void SetPackageMessage()
    {
        _VerMessage.text = string.Format("{0} - {1} - {2}", StaticData.PackageName, StaticData.PackageVer, StaticData.PackageTime);
    }

    public GameStateMgr GetGameStateMgr() { return _gameStateMgr; }

    void InGame(GameState state)
    {
        switch (state)
        {
            case GameState.InWelcome:
                _UIController.OpenUIWelcome();
                _UIController.CloseUISetting();
                _UIController.CloseUIGameplay();
                break;
            case GameState.InGameplay:
                _UIController.OpenUIGameplay();
                _UIController.CloseUIWelcome();
                _UIController.CloseUISetting();
                break;
            case GameState.InSetting:
                _UIController.OpenUISetting();
                _UIController.CloseUIWelcome();
                _UIController.CloseUIGameplay();
                break;
            default:
                Debug.LogWarning("Game state has not set to possible one.");
                break;
        }
    }
}
