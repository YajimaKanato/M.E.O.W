using Interface;
using System.Collections;
using UnityEngine;

/// <summary>イベントに関する制御を行うクラス</summary>
public class EventManager : InitializeBehaviour
{
    [SerializeField] PlayerDataOnPlayScene _player;
    [SerializeField] DogData _dog;
    [SerializeField] CatData _cat;
    [SerializeField] MouseData _mouse;
    [SerializeField] AndroidData _android;
    ObjectManager _dataManager;
    UIManager _uiManager;

    public PlayerDataOnPlayScene Player => _player;
    public DogData Dog => _dog;
    public CatData Cat => _cat;
    public MouseData Mouse => _mouse;
    public AndroidData Android => _android;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _dataManager, _gameManager.ObjectManager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        InitializeManager.InitializationForVariable(out _objectManager, _gameManager.ObjectManager);
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
            Debug.Log($"Get => {item}");
        }
        else if (index == -1)
        {
            Debug.Log("Must Change Item");
            return false;
        }
        else
        {
            _uiManager.SlotUpdate((UsableItem)item, index);
            Debug.Log($"Get => {item}");
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
}
