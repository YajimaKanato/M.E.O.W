using UnityEngine;

public class StatusManager : InitializeBehaviour
{
    [SerializeField] PlayerInfo _player;
    [SerializeField] DogEventData _dog;
    [SerializeField] CatEventData _cat;
    [SerializeField] MouseEventData _mouse;
    [SerializeField] AndroidEventData _android;
    [SerializeField] TrashCanEventData _trashCan;

    public PlayerInfo Player => _player;
    public DogEventData Dog => _dog;
    public CatEventData Cat => _cat;
    public MouseEventData Mouse => _mouse;
    public AndroidEventData Android => _android;
    public TrashCanEventData TrashCan => _trashCan;

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

    static StatusManager _instance;

    public override bool Init(GameManager manager)
    {
        if (_instance == null)
        {
            _instance = this;
            _playerRunTime = new PlayerRunTime(_player);
            _dogEvent = new DogEventRunTime(_dog);
            _catEvent = new CatEventRunTime(_cat);
            _mouseEvent = new MouseEventRunTime(_mouse);
            _androidEvent = new AndroidEventRunTime(_android);
            _trashCanEvent = new TrashCanEventRunTime(_trashCan);
        }
        return true;
    }
}
