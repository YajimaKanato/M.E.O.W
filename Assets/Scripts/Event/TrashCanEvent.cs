using UnityEngine;
using Interface;
using System.Collections;

public class TrashCanEvent : EventBase, IGiveItemInteract
{
    [SerializeField, TextArea] string _itemGiveLog;
    [SerializeField, TextArea] string _alreadyGaveLog;
    [SerializeField] ItemInfo _item;
    public IItemBase Item => _item.ItemBase();

    protected override void EventSetting()
    {
        _eventEnumerator.Enqueue(GiveItem);
        _eventEnumerator.Enqueue(AlreadyGaveItem);
    }

    /// <summary>
    /// アイテムを与えるイベントフローを行う関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    /// <returns></returns>
    IEnumerator GiveItem(PlayerInfo player)
    {
        _interactUIManager.MessageOpen();
        _interactUIManager.MessageTextUpdate(_itemGiveLog);
        yield return null;
        //アイテムを与える
        Debug.Log($"Give => {Item}");
        _interactUIManager.GetItemUIOpen(Item);
        _gameActionManager.GiveItemInteract(this, player);
        yield return null;
        _interactUIManager.GetItemUIClose();
        _interactUIManager.MessageClose();
        NextEvent();
    }

    /// <summary>
    /// アイテムをすでに与えているときのイベントフローを行う関数
    /// </summary>
    /// /// <param name="player">プレイヤーの情報</param>
    /// <returns></returns>
    IEnumerator AlreadyGaveItem(PlayerInfo player)
    {
        _interactUIManager.MessageOpen();
        _interactUIManager.MessageTextUpdate(_alreadyGaveLog);
        yield return null;
        _interactUIManager.MessageClose();
    }
}
