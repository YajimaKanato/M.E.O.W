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
        _initManager.InteractUIManager.MessageOpen();
        _initManager.InteractUIManager.MessageTextUpdate(_itemGiveLog);
        yield return null;
        //アイテムを与える
        Debug.Log($"Give => {Item}");
        _initManager.InteractUIManager.GetItemUIOpen(this);
        _initManager.GameActionManager.GiveItemInteract(this, player);
        yield return null;
        _initManager.InteractUIManager.GetItemUIClose();
        _initManager.InteractUIManager.MessageClose();
        NextEvent();
    }

    /// <summary>
    /// アイテムをすでに与えているときのイベントフローを行う関数
    /// </summary>
    /// /// <param name="player">プレイヤーの情報</param>
    /// <returns></returns>
    IEnumerator AlreadyGaveItem(PlayerInfo player)
    {
        _initManager.InteractUIManager.MessageOpen();
        _initManager.InteractUIManager.MessageTextUpdate(_alreadyGaveLog);
        yield return null;
        _initManager.InteractUIManager.MessageClose();
    }
}
