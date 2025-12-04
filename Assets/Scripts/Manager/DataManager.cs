using Interface;
using Item;
using RunTime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ステータスに関する制御を行うクラス</summary>
public class DataManager : InitializeBehaviour
{
    [SerializeField] ItemDataList _itemDataList;
    [SerializeField] EventDataList _eventDataList;
    [SerializeField] UIDataList _uiDataList;
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
    static ChangeItemRunTime _changeItemRunTime;

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
    public ChangeItemRunTime ChangeItemRunTime => _changeItemRunTime;

    static DataManager _instance;
    UIManager _uiManager;
    List<CharacterNPC> _targetList;
    CharacterNPC _preTarget;
    CharacterNPC _target;
    public CharacterNPC Target => _target;

    public override bool Init(GameManager manager)
    {
        if (_instance == null)
        {
            _instance = this;
            InitializeManager.InitializationForVariable(out _gameManager, manager);
            InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
            InitializeManager.InitializationForVariable(out _targetList, new List<CharacterNPC>());
            //アイテムの初期化
            if (!_itemDataList || !_itemDataList.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
            //UIの初期化
            if (!_uiDataList || !_uiDataList.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
            //UIのランタイムデータを生成
            if (_hotbar) InitializeManager.InitializationForVariable(out _hotbarRunTime, new HotbarRunTime(_hotbar));
            if (_menu) InitializeManager.InitializationForVariable(out _menuRunTime, new MenuRunTime(_menu));
            if (_title) InitializeManager.InitializationForVariable(out _titleRunTime, new TitleRunTime(_title));
            if (_conversation) InitializeManager.InitializationForVariable(out _conversationRunTime, new ConversationRunTime(_conversation));
            if (_message) InitializeManager.InitializationForVariable(out _messageRunTime, new MessageRunTime(_message));
            if (_hotbar) InitializeManager.InitializationForVariable(out _changeItemRunTime, new ChangeItemRunTime(_hotbar));
            //プレイヤーの初期化
            if (!_playerOnPlayScene || !_playerOnPlayScene.Init(manager)) InitializeManager.FailedInitialization();
            //プレイヤーのランタイムデータを生成
            if (_playerOnPlayScene) InitializeManager.InitializationForVariable(out _playerRunTimeOnPlayScene, new PlayerRunTimeOnPlayScene(_playerOnPlayScene));
            //イベントデータの初期化
            if (!_eventDataList || !_eventDataList.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
            //イベントのランタイムデータを生成
            if (_dog) InitializeManager.InitializationForVariable(out _dogRunTime, new DogEventRunTime(_dog));
            if (_cat) InitializeManager.InitializationForVariable(out _catRunTime, new CatEventRunTime(_cat));
            if (_mouse) InitializeManager.InitializationForVariable(out _mouseRunTime, new MouseEventRunTime(_mouse));
            if (_android) InitializeManager.InitializationForVariable(out _androidRunTime, new AndroidEventRunTime(_android));
            if (_trashCan) InitializeManager.InitializationForVariable(out _trashCanRunTime, new TrashCanEventRunTime(_trashCan));
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

    /// <summary>
    /// 会話に関する設定
    /// </summary>
    /// <param name="left">左側に表示するキャラクターのデータ</param>
    /// <param name="right">右側に表示するキャラクターのデータ</param>
    public void ConversationSetting(RunTimeData left, RunTimeData right)
    {
        _conversationRunTime.CharacterDataSetting(GetEventRunTimeData(left), GetEventRunTimeData(right));
    }

    /// <summary>
    /// 指定したキャラクターのデータを取得する関数
    /// </summary>
    /// <param name="runTime">取得するデータ</param>
    /// <returns>データ</returns>
    ITalkable GetEventRunTimeData(RunTimeData runTime)
    {
        switch (runTime)
        {
            case RunTimeData.Player:
                return _playerRunTimeOnPlayScene;
            case RunTimeData.Dog:
                return _dogRunTime;
            case RunTimeData.Cat:
                return _catRunTime;
            case RunTimeData.Mouse:
                return _mouseRunTime;
            case RunTimeData.Android:
                return _androidRunTime;
            default:
                return null;
        }
    }

    /// <summary>
    /// アイテムを受け取る関数
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <returns>アイテムを獲得できたかどうか</returns>
    public IEnumerator GetItem(ItemInfo item)
    {
        _uiManager.OpenGetItem();
        if (item.ItemRole == ItemRole.KeyItem)
        {
            //アイテムリスト
            Debug.Log($"Get => {item}");
            return null;
        }
        else
        {
            var index = _hotbarRunTime.GetItem((UsableItem)item);
            if (index != -1)
            {
                _uiManager.SlotUpdate((UsableItem)item, index);
                Debug.Log($"Get => {item}");
                return null;
            }
            else
            {
                Debug.Log("Must Change Item");
                IEnumerator ChangeItemUIFlow()
                {
                    _uiManager.OpenItemChange();
                    yield return null;
                    _uiManager.UIClose();
                }
                return ChangeItemUIFlow();
            }
        }
    }

    /// <summary>
    /// プレイヤーの体力を管理する関数
    /// </summary>
    /// <param name="health">IHealthを実装したスクリプトのインスタンス</param>
    public void ChangeHealth(IHealth health)
    {
        _playerRunTimeOnPlayScene.ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISatuateを実装したスクリプトのインスタンス</param>
    public void ChangeFullness(ISaturate saturate)
    {
        _playerRunTimeOnPlayScene.Saturation(saturate.Saturate);
    }

    /// <summary>
    /// ターゲットのリストに登録する関数
    /// </summary>
    /// <param name="target">登録するターゲット</param>
    public void AddTargetList(CharacterNPC target)
    {
        _targetList.Add(target);
    }

    /// <summary>
    /// ターゲットのリストから削除する関数
    /// </summary>
    /// <param name="target">削除するターゲット</param>
    public void RemoveTargetList(CharacterNPC target)
    {
        _targetList.Remove(target);
    }

    /// <summary>
    /// 一番近いターゲットを返す関数
    /// </summary>
    /// <param name="position">ターゲットとの距離を測る対象</param>
    public void GetTarget(Transform position)
    {
        _target = null;
        foreach (CharacterNPC go in _targetList)
        {
            if (_target)
            {
                if (Vector3.SqrMagnitude(position.position - _target.transform.position) > Vector3.SqrMagnitude(position.position - go.transform.position))
                {
                    _target = go;
                }
            }
            else
            {
                _target = go;
            }
        }

        //ターゲットの切り替わりを視覚的に変化
        if (_preTarget != _target)
        {
            _preTarget?.TargetSignInactive();
            _target?.TargetSignActive();
            _preTarget = _target;
        }
    }

}

namespace RunTime
{
    public enum RunTimeData
    {
        Player,
        Dog,
        Cat,
        Mouse,
        Android,
        Unknown
    }
}
