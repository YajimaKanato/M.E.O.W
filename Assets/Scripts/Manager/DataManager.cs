using UnityEngine;

/// <summary>ステータスに関する制御を行うクラス</summary>
public class DataManager : InitializeBehaviour
{
    [SerializeField] PlayerInfo _player;
    [SerializeField] ItemDataList _itemDataList;
    [SerializeField] DogEventData _dog;
    [SerializeField] CatEventData _cat;
    [SerializeField] MouseEventData _mouse;
    [SerializeField] AndroidEventData _android;
    [SerializeField] TrashCanEventData _trashCan;
    public PlayerInfo Player => _player;

    PlayerRunTime _playerRunTime;
    DogEventRunTime _dogEvent;
    CatEventRunTime _catEvent;
    MouseEventRunTime _mouseEvent;
    AndroidEventRunTime _androidEvent;
    TrashCanEventRunTime _trashCanEvent;

    public PlayerRunTime PlayerRunTime => _playerRunTime;
    public DogEventRunTime DogEvent => _dogEvent;
    public CatEventRunTime CatEvent => _catEvent;
    public MouseEventRunTime MouseEvent => _mouseEvent;
    public AndroidEventRunTime AndroidEvent => _androidEvent;
    public TrashCanEventRunTime TrashCanEvent => _trashCanEvent;

    static DataManager _instance;

    public override bool Init(GameManager manager)
    {
        if (_instance == null)
        {
            _instance = this;
            if (!_itemDataList.Init(manager)) FailedInitialization();
            if (!_player.Init(manager)) FailedInitialization();
            if (!_dog.Init(manager)) FailedInitialization();
            if (!_cat.Init(manager)) FailedInitialization();
            if (!_mouse.Init(manager)) FailedInitialization();
            if (!_android.Init(manager)) FailedInitialization();
            if (!_trashCan.Init(manager)) FailedInitialization();
            _playerRunTime = new PlayerRunTime(_player);
            if (_playerRunTime == null) FailedInitialization();
            _dogEvent = new DogEventRunTime(_dog);
            if (_dogEvent == null) FailedInitialization();
            _catEvent = new CatEventRunTime(_cat);
            if (_catEvent == null) FailedInitialization();
            _mouseEvent = new MouseEventRunTime(_mouse);
            if (_mouseEvent == null) FailedInitialization();
            _androidEvent = new AndroidEventRunTime(_android);
            if (_androidEvent == null) FailedInitialization();
            _trashCanEvent = new TrashCanEventRunTime(_trashCan);
            if (_trashCanEvent == null) FailedInitialization();
        }
        return _isInitialized;
    }
}
