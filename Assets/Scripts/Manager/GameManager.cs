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
            Initialize(_dataManager);
            Initialize(_gameFlowManager);
            Initialize(_playerInputActionManager);
            Initialize(_outGameActionManager);
            Initialize(_outGameUIManager);
            Initialize(_gameActionManager);
            Initialize(_uiManager);
        }

        if (_initObj == null) Debug.LogWarning("Initialize Object is null");
        foreach (var initObj in _initObj)
        {
            if (Initialize(initObj.Obj))
            {
                initObj.Obj.gameObject.SetActive(initObj.Active);
            }
        }
    }

    /// <summary>
    /// 初期化を行う関数
    /// </summary>
    /// <param name="init">初期化するインスタンス</param>
    bool Initialize(InitializeBehaviour init)
    {
        if (!init)
        {
            Debug.Log($"{init} is null");
            return false;
        }

        if (init.Init(this))
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
}
