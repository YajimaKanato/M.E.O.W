using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ゴミ箱のイベントデータ</summary>
[CreateAssetMenu(fileName = "TrashCanEvent", menuName = "Event/GiveItem/TrashCanEvent")]
public class TrashCanEventData : EventBaseData
{
    [SerializeField, Tooltip("アイテム")] UsableItem _item;
    [SerializeField, Tooltip("アイテムを与える時に表示するテキスト"), TextArea] string _itemGiveLog;
    [SerializeField, Tooltip("アイテムをすでに与えているときに表示するテキスト"), TextArea] string _alreadyGaveLog;

    public override bool Init(GameManager manager)
    {
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _eventManager, _gameManager.EventManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _eventEnumerator, new Queue<Func<IEnumerator>>());
        if (!EventSetting()) _isInitialized = InitializeManager.FailedInitialization();
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
    /// <returns></returns>
    IEnumerator GiveItem()
    {
        _eventManager.StartMessage(_itemGiveLog, 0);
        yield return null;
        //アイテムを与える
        if (!_eventManager.GiveItem(_item))
        {
            yield return null;
            _uiManager.OpenItemChange(_item);
        }
        yield return null;
        _uiManager.UIClose();
        NextEvent();
    }

    /// <summary>
    /// アイテムをすでに与えているときのイベントフローを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator AlreadyGaveItem()
    {
        _eventManager.StartMessage(_alreadyGaveLog, 0);
        yield return null;
        _uiManager.UIClose();
    }
}

#region TrashCan
/// <summary>ゴミ箱のランタイムデータ</summary>
public class TrashCanEventRunTime : EventRunTime, IRunTime
{
    TrashCanEventData _trashCanEventData;

    public TrashCanEventRunTime(TrashCanEventData data)
    {
        _trashCanEventData = data;
        _eventEnumerator = _trashCanEventData.EventEnumerator;
    }

    public override IEnumerator Event()
    {
        if (_eventEnumerator == null)
        {
            Debug.Log("Event Enumerator is null");
            return null;
        }

        //イベントが登録されている
        if (_eventEnumerator.Count > 0)
        {
            //現在行うイベントが登録されていない
            if (_trashCanEventData.IsNext)
            {
                _currentEnumerator = _eventEnumerator.Dequeue();
            }
        }
        else
        {
            Debug.Log("There are no Events");
        }

        if (_currentEnumerator != null)
        {
            Debug.Log("Event Registering");
            return _currentEnumerator();
        }
        else
        {
            return null;
        }
    }
}
#endregion
