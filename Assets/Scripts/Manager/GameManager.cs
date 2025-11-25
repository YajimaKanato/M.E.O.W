using UnityEngine;

/// <summary>マネージャークラスを統括するクラス</summary>
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    class InitializeObject
    {
        [SerializeField] InitializeBehaviour _obj;
        [SerializeField] bool _active;

        public InitializeBehaviour Obj => _obj;
        public bool Active => _active;
    }

    [Header("Manager")]
    [SerializeField] GameActionManager _gameActionManager;
    [SerializeField] GameFlowManager _gameFlowManager;
    [SerializeField] StatusManager _statusManager;
    [SerializeField] PlayerInputActionManager _playerInputActionManager;
    [SerializeField] InteractUIManager _interactUIManager;
    [Header("Initialize Object")]
    //[SerializeField] ItemDataList _itemDataList;
    //[SerializeField] Hotbar _hotbar;
    //[SerializeField] ItemList _itemList;
    //[SerializeField] InitializeBehaviour[] _enableObj;
    //[SerializeField] InitializeBehaviour[] _disableObj;
    [SerializeField] InitializeObject[] _initObj;

    public GameActionManager GameActionManager => _gameActionManager;
    public GameFlowManager GameFlowManager => _gameFlowManager;
    public StatusManager StatusManager => _statusManager;
    public PlayerInputActionManager PlayerInputActionManager => _playerInputActionManager;
    public InteractUIManager InteractUIManager => _interactUIManager;
    //public ItemDataList ItemDataList => _itemDataList;
    //public Hotbar Hotbar => _hotbar;
    //public ItemList ItemList => _itemList;
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
    void Initialize(InitializeBehaviour init)
    {
        if (!init)
        {
            Debug.Log($"{init} is null");
            return;
        }

        Debug.Log($"{init}'s Initialization is " + (init.Init(this) ? "Success" : "Failed"));
    }

    private void Start()
    {

    }
}
