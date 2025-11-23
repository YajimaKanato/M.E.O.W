using UnityEngine;

public class StatusManager : InitializeBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] DogEventData _dog;
    [SerializeField] CatEventData _cat;
    [SerializeField] MouseEventData _mouse;
    [SerializeField] AndroidEventData _android;
    [SerializeField] TrashCanEventData _trashCan;

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
    public static StatusManager Instance => _instance;

    public override void Init(GameManager manager)
    {
        if (_instance == null)
        {
            _instance = this;
            _playerRunTime = new PlayerRunTime(_playerInfo);
            _dogEvent = new DogEventRunTime(_dog);
            _catEvent = new CatEventRunTime(_cat);
            _mouseEvent = new MouseEventRunTime(_mouse);
            _androidEvent = new AndroidEventRunTime(_android);
            _trashCanEvent = new TrashCanEventRunTime(_trashCan);
            Debug.Log($"{this} has Initialized");
        }
    }
}
