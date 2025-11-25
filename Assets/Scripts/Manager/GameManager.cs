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
    [SerializeField] GameActionManager _gameActionManager;
    [SerializeField] GameFlowManager _gameFlowManager;
    [SerializeField] StatusManager _statusManager;
    [SerializeField] PlayerInputActionManager _playerInputActionManager;
    [SerializeField] UIManager _interactUIManager;
    [Header("Initialize Object")]
    //[SerializeField] InitializeBehaviour[] _enableObj;
    //[SerializeField] InitializeBehaviour[] _disableObj;
    [SerializeField] InitializeObject[] _initObj;

    public GameActionManager GameActionManager => _gameActionManager;
    public GameFlowManager GameFlowManager => _gameFlowManager;
    public StatusManager StatusManager => _statusManager;
    public PlayerInputActionManager PlayerInputActionManager => _playerInputActionManager;
    public UIManager InteractUIManager => _interactUIManager;
    static GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Initialize(_gameActionManager);
            Initialize(_gameFlowManager);
            Initialize(_statusManager);
            Initialize(_playerInputActionManager);
            Initialize(_interactUIManager);
        }

        foreach (var initObj in _initObj)
        {
            if(Initialize(initObj.Obj))
            {
                initObj.Obj.gameObject.SetActive(initObj.Active);
            }
        }

        //foreach (var obj in _enableObj)
        //{
        //    Initialize(obj);
        //    obj?.gameObject.SetActive(true);
        //}
        //foreach (var obj in _disableObj)
        //{
        //    Initialize(obj);
        //    obj?.gameObject.SetActive(false);
        //}
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
