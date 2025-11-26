using UnityEngine;

/// <summary>ステータスに関する制御を行うクラス</summary>
public class DataManager : InitializeBehaviour
{
    [SerializeField] PlayerInfo _player;
    [SerializeField] DogEventData _dog;
    [SerializeField] CatEventData _cat;
    [SerializeField] MouseEventData _mouse;
    [SerializeField] AndroidEventData _android;
    [SerializeField] TrashCanEventData _trashCan;
    [SerializeField] ItemDataList _itemDataList;
    public PlayerInfo Player => _player;
    public DogEventData Dog => _dog;
    public CatEventData Cat => _cat;
    public MouseEventData Mouse => _mouse;
    public AndroidEventData Android => _android;
    public TrashCanEventData TrashCan => _trashCan;
    public ItemDataList ItemDataList => _itemDataList;

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
            if (!_player.Init(manager)) return false;
            if (!_dog.Init(manager)) return false;
            //if(!_cat.Init(manager))return false;
            //if(!_mouse.Init(manager))return false;
            //if(!_android.Init(manager))return false;
            //if(!_trashCan.Init(manager))return false;
            _playerRunTime = new PlayerRunTime(_player);
            if (_playerRunTime == null) return false;
            _dogEvent = new DogEventRunTime(_dog);
            if (_dogEvent == null) return false;
            _catEvent = new CatEventRunTime(_cat);
            if (_catEvent == null) return false;
            _mouseEvent = new MouseEventRunTime(_mouse);
            if (_mouseEvent == null) return false;
            _androidEvent = new AndroidEventRunTime(_android);
            if (_androidEvent == null) return false;
            _trashCanEvent = new TrashCanEventRunTime(_trashCan);
            if (_trashCanEvent == null) return false;

        }
        return true;
    }
}
