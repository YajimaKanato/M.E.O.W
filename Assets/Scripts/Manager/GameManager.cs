using UnityEngine;

/// <summary>マネージャークラスを統括するクラス</summary>
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    class InitializeObject
    {
        [SerializeField] InitializeBehaviour _obj;
        [SerializeField] bool _active = true;

        public InitializeBehaviour Obj => _obj;
        public bool Active => _active;
    }

    [Header("Manager")]
    [SerializeField] DataManager _dataManager;
    [SerializeField] GameFlowManager _gameFlowManager;
    [SerializeField] PlayerInputActionManager _playerInputActionManager;
    [SerializeField] OutGameActionManager _outGameActionManager;
    [SerializeField] OutGameUIManager _outGameUIManager;
    [SerializeField] GameActionManager _gameActionManager;
    [SerializeField] UIManager _uiManager;
    [Header("Initialize Object")]
    [SerializeField] InitializeObject[] _initObj;

    public DataManager DataManager => _dataManager;
    public GameFlowManager GameFlowManager => _gameFlowManager;
    public PlayerInputActionManager PlayerInputActionManager => _playerInputActionManager;
    public OutGameActionManager OutGameActionManager => _outGameActionManager;
    public OutGameUIManager OutGameUIManager => _outGameUIManager;
    public GameActionManager GameActionManager => _gameActionManager;
    public UIManager UIManager => _uiManager;

    static GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            InitializeManager.Initialize(_dataManager, _instance);
            InitializeManager.Initialize(_gameFlowManager, _instance);
            InitializeManager.Initialize(_playerInputActionManager, _instance);
            InitializeManager.Initialize(_outGameActionManager, _instance);
            InitializeManager.Initialize(_outGameUIManager, _instance);
            InitializeManager.Initialize(_gameActionManager, _instance);
            InitializeManager.Initialize(_uiManager, _instance);
        }

        if (_initObj == null) Debug.LogWarning("Initialize Object is null");
        foreach (var initObj in _initObj)
        {
            if (InitializeManager.Initialize(initObj.Obj, _instance))
            {
                initObj.Obj.gameObject.SetActive(initObj.Active);
            }
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
    public static bool Initialize(InitializeBehaviour init, GameManager manager)
    {
        if (!init)
        {
            Debug.Log($"{init} is null");
            return false;
        }

        if (init.Init(manager))
        {
            Debug.Log($"{init}'s Initialization is Success");
            return true;
        }
        else
        {
            Debug.Log($"{init}'s Initialization is Failed");
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
        return true;
    }

    /// <summary>
    /// 初期化が失敗したときに返す関数
    /// </summary>
    public static bool FailedInitialization()
    {
        Debug.Log("Initialization was Failed");
        return false;
    }
}
