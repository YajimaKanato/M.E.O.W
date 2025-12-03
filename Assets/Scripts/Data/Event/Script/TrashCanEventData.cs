using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrashCanEvent", menuName = "Event/GiveItem/TrashCanEvent")]
public class TrashCanEventData : EventBaseData
{
    [SerializeField] ItemInfo _item;
    [SerializeField, TextArea] string _itemGiveLog;
    [SerializeField, TextArea] string _alreadyGaveLog;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        InitializeManager.InitializationForVariable(out _eventEnumerator, new Queue<Func<IEnumerator>>());
        if (!EventSetting()) InitializeManager.FailedInitialization();
        _isNext = true;
        return _isInitialized;
    }

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
        _uiManager.OpenMessage();
        _uiManager.MessageTextUpdate(_itemGiveLog, 0);
        yield return null;
        //アイテムを与える
        _gameManager.GameActionManager.GetItem(_item);
        _uiManager.OpenGetItem();
        yield return null;
        _uiManager.UIClose();
        _uiManager.UIClose();
        NextEvent();
    }

    /// <summary>
    /// アイテムをすでに与えているときのイベントフローを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator AlreadyGaveItem()
    {
        _uiManager.OpenMessage();
        _uiManager.MessageTextUpdate(_alreadyGaveLog, 0);
        yield return null;
        _uiManager.UIClose();
    }
}
