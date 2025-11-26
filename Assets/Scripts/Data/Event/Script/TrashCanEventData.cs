using UnityEngine;
using Interface;
using System.Collections;

[CreateAssetMenu(fileName = "TrashCanEvent", menuName = "Event/GiveItem/TrashCanEvent")]
public class TrashCanEventData : EventBaseData, IGiveItemInteract
{
    [SerializeField] ItemInfo _item;
    [SerializeField, TextArea] string _itemGiveLog;
    [SerializeField, TextArea] string _alreadyGaveLog;
    public IItemBase Item => _item.ItemBase();

    protected override bool EventSetting()
    {
        _eventEnumerator.Enqueue(GiveItem);
        _eventEnumerator.Enqueue(AlreadyGaveItem);
        return _eventEnumerator.Count > 0;
    }

    /// <summary>
    /// アイテムを与えるイベントフローを行う関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    /// <returns></returns>
    IEnumerator GiveItem()
    {
        _gameManager.UIManager.MessageOpen();
        _gameManager.UIManager.MessageTextUpdate(_itemGiveLog);
        yield return null;
        //アイテムを与える
        Debug.Log($"Give => {Item}");
        _gameManager.UIManager.GetItemUIOpen(this);
        _gameManager.GameActionManager.GiveItemInteract(this);
        yield return null;
        _gameManager.UIManager.GetItemUIClose();
        _gameManager.UIManager.MessageClose();
        NextEvent();
    }

    /// <summary>
    /// アイテムをすでに与えているときのイベントフローを行う関数
    /// </summary>
    /// /// <param name="player">プレイヤーの情報</param>
    /// <returns></returns>
    IEnumerator AlreadyGaveItem()
    {
        _gameManager.UIManager.MessageOpen();
        _gameManager.UIManager.MessageTextUpdate(_alreadyGaveLog);
        yield return null;
        _gameManager.UIManager.MessageClose();
    }
}
