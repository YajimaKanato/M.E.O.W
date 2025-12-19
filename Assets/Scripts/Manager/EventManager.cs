using Interface;
using Item;
using UnityEngine;

/// <summary>イベントに関する制御を行うクラス</summary>
public class EventManager : ManagerBase
{
    [SerializeField, Tooltip("プレイヤーのデータ")] PlayerDataOnPlayScene _player;
    [SerializeField, Tooltip("犬のデータ")] DogData _dog;
    [SerializeField, Tooltip("猫のデータ")] CatData _cat;
    [SerializeField, Tooltip("ネズミのデータ")] MouseData _mouse;
    [SerializeField, Tooltip("アンドロイドのデータ")] AndroidData _android;
    UIManager _uiManager;
    ObjectManager _objectManager;

    public PlayerDataOnPlayScene Player => _player;
    public DogData Dog => _dog;
    public CatData Cat => _cat;
    public MouseData Mouse => _mouse;
    public AndroidData Android => _android;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _objectManager, _gameManager.ObjectManager);

        return _isInitialized;
    }

    /// <summary>
    /// 会話を始める関数
    /// </summary>
    /// <param name="left">左側に表示するキャラクターのデータ</param>
    /// <param name="right">右側に表示するキャラクターのデータ</param>
    public void StartConversation(ITalkable left, ITalkable right)
    {
        _uiManager.OpenConversation(left, right);
    }

    /// <summary>
    /// 会話するキャラが変わったときに呼ばれる関数
    /// </summary>
    /// <param name="left">左側に表示するキャラクターのデータ</param>
    /// <param name="right">右側に表示するキャラクターのデータ</param>
    public void ChangeConversation(ITalkable left, ITalkable right)
    {
        _uiManager.ConversationSetting(left, right);
    }

    /// <summary>
    /// メッセージを流す関数
    /// </summary>
    /// <param name="message">流すメッセージ</param>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void StartMessage(string message, int index)
    {
        _uiManager.OpenMessage(message, index);
    }

    /// <summary>
    /// メッセージを更新する関数
    /// </summary>
    /// <param name="message">流すメッセージ</param>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void MessageUpdate(string message, int index)
    {
        Debug.Log("Next Message");
        _uiManager.MessageTextUpdate(message, index);
    }

    /// <summary>
    /// アイテムを与える関数
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <returns>アイテムを獲得できたかどうか</returns>
    public bool GiveItem(ItemInfo item)
    {
        var index = _uiManager.GetItem(item);
        _uiManager.OpenGetItem();
        if (index == -2)
        {
            Debug.Log($"Get <color=yellow>KeyItem</color> => {item}");
        }
        else if (index == -1)
        {
            Debug.Log("Must Change Item");
            return false;
        }
        else
        {
            _uiManager.SlotUpdate((UsableItem)item, index);
            Debug.Log($"Get <color=green>Food</color> => {item}");
        }
        return true;
    }

    /// <summary>
    /// ドロップアイテムを拾う関数
    /// </summary>
    /// <param name="item">拾ったアイテム</param>
    /// <returns>アイテム交換を必要とするかどうか</returns>
    public bool PickItem(UsableItem item)
    {
        var index = _uiManager.GetItem(item);
        _uiManager.OpenGetItem();
        if (index == -1)
        {
            Debug.Log("Must Change Item");
            return false;
        }
        else
        {
            _uiManager.SlotUpdate(item, index);
            _objectManager.GetDropItem(item);
            Debug.Log($"Get => {item}");
        }
        return true;
    }

    /// <summary>
    /// 意思決定イベントを起こす関数
    /// </summary>
    /// <param name="type">特定のアイテムが欲しいかどうか</param>
    /// <param name="item">欲しいアイテムの最適解</param>
    public void DecideEvent(bool type, ItemInfo item)
    {
        _uiManager.OpenDecideUI(type, item);
    }

    /// <summary>
    /// 特定のアイテムを渡すイベントかどうかを返す関数
    /// </summary>
    /// <returns>特定のアイテムを渡すイベントかどうか</returns>
    public bool GiveSpecificItemEvent()
    {
        return _uiManager.IsGiveSpecificItemEvent();
    }

    /// <summary>
    /// 任意のアイテムを渡すイベントを起こす関数
    /// </summary>
    public void GiveAnyItemEvent()
    {
        _uiManager.OpenGiveAnyItem();
    }
}
