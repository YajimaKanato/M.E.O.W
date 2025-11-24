using UnityEngine;

/// <summary>マネージャークラスを統括するクラス</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] GameActionManager _gameActionManager;
    [SerializeField] GameFlowManager _gameFlowManager;
    [SerializeField] PlayerInputActionManager _playerInputActionManager;
    [SerializeField] InteractUIManager _interactUIManager;
    [SerializeField] ItemDataList _itemDataList;
    [SerializeField] Hotbar _hotbar;
    [SerializeField] ItemList _itemList;
    [SerializeField] InitializeObject[] _objs;

    static GameManager _instance;
    public static GameManager Instance => _instance;
    public GameActionManager GameActionManager => _gameActionManager;
    public GameFlowManager GameFlowManager => _gameFlowManager;
    public PlayerInputActionManager PlayerInputActionManager => _playerInputActionManager;
    public InteractUIManager InteractUIManager => _interactUIManager;
    public ItemDataList ItemDataList => _itemDataList;
    public Hotbar Hotbar => _hotbar;
    public ItemList ItemList => _itemList;

    private void Awake()
    {
        _instance = this;
        _gameActionManager?.Init(this);
        _gameFlowManager?.Init(this);
        _itemDataList?.Init(this);
        _hotbar?.Init(this);
        _itemList?.Init(this);
        foreach (var item in _objs)
        {
            item?.Init(this);
        }
    }

    private void Start()
    {

    }
}
