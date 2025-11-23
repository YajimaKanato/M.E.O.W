using UnityEngine;

/// <summary>マネージャークラスを統括するクラス</summary>
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    class SceneObject
    {
        [SerializeField] InitializeBehaviour _obj;

    }

    [SerializeField] InitializeBehaviour[] _enableObj;
    [SerializeField] InitializeBehaviour[] _disableObj;
    [SerializeField] GameActionManager _gameActionManager;
    [SerializeField] GameFlowManager _gameFlowManager;
    [SerializeField] StatusManager _statusManager;
    [SerializeField] PlayerInputActionManager _playerInputActionManager;
    [SerializeField] InteractUIManager _interactUIManager;
    [SerializeField] ItemDataList _itemDataList;
    [SerializeField] Hotbar _hotbar;
    [SerializeField] ItemList _itemList;

    public GameActionManager GameActionManager => _gameActionManager;
    public GameFlowManager GameFlowManager => _gameFlowManager;
    public StatusManager StatusManager => _statusManager;
    public PlayerInputActionManager PlayerInputActionManager => _playerInputActionManager;
    public InteractUIManager InteractUIManager => _interactUIManager;
    public ItemDataList ItemDataList => _itemDataList;
    public Hotbar Hotbar => _hotbar;
    public ItemList ItemList => _itemList;

    private void Awake()
    {
        _gameActionManager?.Init(this);
        _gameFlowManager?.Init(this);
        _statusManager?.Init(this);
        _playerInputActionManager?.Init(this);
        _interactUIManager?.Init(this);
        foreach (var obj in _enableObj)
        {
            obj?.Init(this);
            obj?.gameObject.SetActive(true);
        }
        foreach (var obj in _disableObj)
        {
            obj?.Init(this);
            obj?.gameObject.SetActive(false);
        }
    }

    private void Start()
    {

    }
}
