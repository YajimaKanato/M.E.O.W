using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInstanceData", menuName = "Item/ItemInstanceData")]
public class ItemInstanceData : EventBaseData
{
    /// <summary>イベントを保存しておくキュー</summary>
    protected Queue<Func<ItemInfo, IEnumerator>> _event;
    public Queue<Func<ItemInfo, IEnumerator>> Event => _event;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        InitializeManager.InitializationForVariable(out _eventManager, _gameManager.EventManager);
        InitializeManager.InitializationForVariable(out _event, new Queue<Func<ItemInfo, IEnumerator>>());
        if (!EventSetting()) InitializeManager.FailedInitialization();
        return _isInitialized;
    }

    protected override bool EventSetting()
    {
        _event.Enqueue(GetItemEvent);
        return _event.Count > 0;
    }

    IEnumerator GetItemEvent(ItemInfo item)
    {
        //アイテムを与える
        if (!_eventManager.GiveItem(item))
        {
            yield return null;
            _uiManager.OpenItemChange((UsableItem)item);
        }
        yield return null;
        _uiManager.UIClose();
    }
}

public class ItemInstanceRunTime : EventRunTime, IRunTime
{
    Queue<Func<ItemInfo, IEnumerator>> _event;
    Func<ItemInfo, IEnumerator> _current;
    ItemInstanceData _itemInstanceData;
    UsableItem _item;
    public UsableItem Item => _item;
    public ItemInstanceRunTime(ItemInstanceData info)
    {
        _itemInstanceData = info;
        _event = _itemInstanceData.Event;
    }

    /// <summary>
    /// 表示するアイテムの情報を設定する関数
    /// </summary>
    /// <param name="item">アイテムの情報</param>
    public void ItemDataSetting(UsableItem item)
    {
        _item = item;
    }

    public override IEnumerator Event()
    {
        if (_event == null)
        {
            Debug.Log("Event Enumerator is null");
            return null;
        }

        //イベントが登録されている
        if (_event.Count > 0)
        {
            //現在行うイベントが登録されていない
            if (_current == null)
            {
                _current = _event.Dequeue();
                Debug.Log("Event Dequeue");
            }
        }
        else
        {
            Debug.Log("There are no Events");
        }

        if (_current != null)
        {
            Debug.Log("Event Registering");
            return _current(_item);
        }
        else
        {
            return null;
        }
    }
}
