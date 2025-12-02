using UnityEngine;
using Interface;
using System.Collections;

[CreateAssetMenu(fileName = "TrashCanEvent", menuName = "Event/GiveItem/TrashCanEvent")]
public class TrashCanEventData : EventBaseData
{
    [SerializeField] ItemInfo _item;
    [SerializeField, TextArea] string _itemGiveLog;
    [SerializeField, TextArea] string _alreadyGaveLog;

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
        _gameManager.UIManager.OpenMessage();
        _gameManager.UIManager.MessageTextUpdate(_itemGiveLog, 0);
        yield return null;
        //アイテムを与える
        _gameManager.GameActionManager.GetItem(_item);
        _gameManager.UIManager.OpenGetItem();
        yield return null;
        _gameManager.UIManager.UIClose();
        _gameManager.UIManager.UIClose();
        NextEvent();
    }

    /// <summary>
    /// アイテムをすでに与えているときのイベントフローを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator AlreadyGaveItem()
    {
        _gameManager.UIManager.OpenMessage();
        _gameManager.UIManager.MessageTextUpdate(_alreadyGaveLog, 0);
        yield return null;
        _gameManager.UIManager.UIClose();
    }
}
