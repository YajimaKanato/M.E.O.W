using UnityEngine;

/// <summary>マネージャークラスを統括するクラス</summary>
public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] RuntimeDataManager _runtimeDataManager;
    [SerializeField] EventManager _eventManager;
    [SerializeField] GameFlowManager _gameFlowManager;
    [SerializeField] PlayerInputActionManager _playerInputActionManager;
    [SerializeField] OutGameActionManager _outGameActionManager;
    [SerializeField] OutGameUIManager _outGameUIManager;
    [SerializeField] GameActionManager _gameActionManager;
    [SerializeField] UIManager _uiManager;
    [SerializeField] ObjectManager _objectManager;

    public RuntimeDataManager RuntimeDataManager => _runtimeDataManager;
    public EventManager EventManager => _eventManager;
    public GameFlowManager GameFlowManager => _gameFlowManager;
    public PlayerInputActionManager PlayerInputActionManager => _playerInputActionManager;
    public OutGameActionManager OutGameActionManager => _outGameActionManager;
    public OutGameUIManager OutGameUIManager => _outGameUIManager;
    public GameActionManager GameActionManager => _gameActionManager;
    public UIManager UIManager => _uiManager;
    public ObjectManager ObjectManager => _objectManager;

    static GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            InitializeManager.InitializationForManager(_runtimeDataManager, _instance);
            InitializeManager.InitializationForManager(_eventManager, _instance);
            InitializeManager.InitializationForManager(_gameFlowManager, _instance);
            InitializeManager.InitializationForManager(_playerInputActionManager, _instance);
            InitializeManager.InitializationForManager(_outGameUIManager, _instance);
            InitializeManager.InitializationForManager(_outGameActionManager, _instance);
            InitializeManager.InitializationForManager(_uiManager, _instance);
            InitializeManager.InitializationForManager(_gameActionManager, _instance);
            InitializeManager.InitializationForManager(_objectManager, _instance);
        }
    }
}

/// <summary>初期化に関する処理を司るクラス</summary>
public static class InitializeManager
{
    /// <summary>
    /// 初期化を行う関数
    /// </summary>
    /// <param name="init">初期化するインスタンス</param>
    /// <param name="manager">マネージャークラスのインスタンス</param>
    public static bool InitializationForManager<T>(T init, GameManager manager) where T : ManagerBase
    {
        if (!init)
        {
            Debug.Log($"{typeof(T)} is Null");
            return false;
        }

        if (init.Init(manager))
        {
            Debug.Log($"{init}'s Initialization is <color=yellow>Success</color>");
            return true;
        }
        else
        {
            Debug.Log($"{init}'s Initialization is <color=red>Failed</color>");
            return false;
        }
    }

    /// <summary>
    /// 任意の変数に対して初期化を行う関数
    /// </summary>
    /// <typeparam name="T">初期化を行う変数の型</typeparam>
    /// <param name="variable">初期化を行う変数</param>
    /// <param name="instance">初期化時に必要なインスタンス</param>
    public static bool InitializationForVariable<T>(out T variable, T instance) where T : class
    {
        variable = instance;
        if (variable == null) return FailedInitialization();
        Debug.Log("Initialization was <color=yellow>Success</color>");
        return true;
    }

    /// <summary>
    /// 初期化が失敗したときに返す関数
    /// </summary>
    public static bool FailedInitialization()
    {
        Debug.Log("Initialization was <color=red>Failed</color>");
        return false;
    }
}
