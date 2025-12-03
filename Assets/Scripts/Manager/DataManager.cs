using UnityEngine;

/// <summary>ステータスに関する制御を行うクラス</summary>
public class DataManager : InitializeBehaviour
{
    [SerializeField] ItemListData _itemDataList;
    [SerializeField] EventDataList _eventDataList;
    [SerializeField] PlayerDataOnPlayScene _playerOnPlayScene;
    [SerializeField] DogEventData _dog;
    [SerializeField] CatEventData _cat;
    [SerializeField] MouseEventData _mouse;
    [SerializeField] AndroidEventData _android;
    [SerializeField] TrashCanEventData _trashCan;
    [SerializeField] HotbarData _hotbar;
    [SerializeField] MenuData _menu;
    [SerializeField] TitleData _title;
    [SerializeField] ConversationData _conversation;
    [SerializeField] MessageData _message;

    static PlayerRunTimeOnPlayScene _playerRunTimeOnPlayScene;
    static DogEventRunTime _dogRunTime;
    static CatEventRunTime _catRunTime;
    static MouseEventRunTime _mouseRunTime;
    static AndroidEventRunTime _androidRunTime;
    static TrashCanEventRunTime _trashCanRunTime;
    static HotbarRunTime _hotbarRunTime;
    static MenuRunTime _menuRunTime;
    static TitleRunTime _titleRunTime;
    static ConversationRunTime _conversationRunTime;
    static MessageRunTime _messageRunTime;

    public PlayerRunTimeOnPlayScene PlayerRunTimeOnPlayScene => _playerRunTimeOnPlayScene;
    public DogEventRunTime DogEvent => _dogRunTime;
    public CatEventRunTime CatEvent => _catRunTime;
    public MouseEventRunTime MouseEvent => _mouseRunTime;
    public AndroidEventRunTime AndroidEvent => _androidRunTime;
    public TrashCanEventRunTime TrashCanEvent => _trashCanRunTime;
    public HotbarRunTime HotbarRunTime => _hotbarRunTime;
    public MenuRunTime MenuRunTime => _menuRunTime;
    public TitleRunTime TitleRunTime => _titleRunTime;
    public ConversationRunTime ConversationRunTime => _conversationRunTime;
    public MessageRunTime MessageRunTime => _messageRunTime;


    static DataManager _instance;

    public override bool Init(GameManager manager)
    {
        if (_instance == null)
        {
            _instance = this;
            _gameManager = manager;
            if (!_itemDataList || !_itemDataList.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
            if (!_eventDataList || !_eventDataList.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
            if (_playerOnPlayScene) InitializeManager.InitializationForVariable(out _playerRunTimeOnPlayScene, new PlayerRunTimeOnPlayScene(_playerOnPlayScene));
            if (_dog) InitializeManager.InitializationForVariable(out _dogRunTime, new DogEventRunTime(_dog));
            if (_cat) InitializeManager.InitializationForVariable(out _catRunTime, new CatEventRunTime(_cat));
            if (_mouse) InitializeManager.InitializationForVariable(out _mouseRunTime, new MouseEventRunTime(_mouse));
            if (_android) InitializeManager.InitializationForVariable(out _androidRunTime, new AndroidEventRunTime(_android));
            if (_trashCan) InitializeManager.InitializationForVariable(out _trashCanRunTime, new TrashCanEventRunTime(_trashCan));
            if (_hotbar) InitializeManager.InitializationForVariable(out _hotbarRunTime, new HotbarRunTime(_hotbar));
            if (_menu) InitializeManager.InitializationForVariable(out _menuRunTime, new MenuRunTime(_menu));
            if (_title) InitializeManager.InitializationForVariable(out _titleRunTime, new TitleRunTime(_title));
            if (_conversation) InitializeManager.InitializationForVariable(out _conversationRunTime, new ConversationRunTime(_conversation));
            if (_message) InitializeManager.InitializationForVariable(out _messageRunTime, new MessageRunTime(_message));
        }
        return _isInitialized;
    }

    /// <summary>
    /// データをリセットする関数
    /// </summary>
    public void DataReset()
    {
        _instance = null;
        Debug.Log($"DataManager has Cleaned");
    }
}
