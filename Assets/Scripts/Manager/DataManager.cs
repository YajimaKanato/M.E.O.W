using Interface;
using System;
using UnityEngine;

/// <summary>ステータスに関する制御を行うクラス</summary>
public class DataManager : InitializeBehaviour
{
    [SerializeField] ItemListData _itemDataList;
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
            if (!_itemDataList || !_itemDataList.Init(manager)) FailedInitialization();
            DataInitialize(_playerOnPlayScene, out _playerRunTimeOnPlayScene, init => new PlayerRunTimeOnPlayScene(init));
            DataInitialize(_dog, out _dogRunTime, init => new DogEventRunTime(init));
            DataInitialize(_cat, out _catRunTime, init => new CatEventRunTime(init));
            DataInitialize(_mouse, out _mouseRunTime, init => new MouseEventRunTime(init));
            DataInitialize(_android, out _androidRunTime, init => new AndroidEventRunTime(init));
            DataInitialize(_trashCan, out _trashCanRunTime, init => new TrashCanEventRunTime(init));
            DataInitialize(_hotbar, out _hotbarRunTime, init => new HotbarRunTime(init));
            DataInitialize(_menu, out _menuRunTime, init => new MenuRunTime(init));
            DataInitialize(_title, out _titleRunTime, init => new TitleRunTime(init));
            DataInitialize(_conversation, out _conversationRunTime, init => new ConversationRunTime(init));
            DataInitialize(_message, out _messageRunTime, init => new MessageRunTime(init));
        }
        return _isInitialized;
    }

    /// <summary>
    /// 初期化とランタイム中のデータ保存を行う関数
    /// </summary>
    /// <typeparam name="TInit">初期化するデータの型</typeparam>
    /// <typeparam name="TEvent">ランタイム中のデータの型</typeparam>
    /// <param name="init">初期化するデータ</param>
    /// <param name="anyEvent">ランタイム中のデータ</param>
    /// <param name="initFunc">ランタイム中のデータの型のコンストラクタ</param>
    void DataInitialize<TInit, TEvent>(TInit init, out TEvent anyEvent, Func<TInit, TEvent> initFunc) where TInit : InitializeSO where TEvent : IRunTime
    {
        anyEvent = default;
        if (!init)
        {
            FailedInitialization();
        }
        else
        {
            if (!init.Init(_gameManager))
            {
                FailedInitialization();
            }
            else
            {
                anyEvent = initFunc(init);
                if (anyEvent == null) FailedInitialization();
            }
        }
    }
}
