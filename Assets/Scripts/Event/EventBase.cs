using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interface;

/// <summary>イベントのベースクラス</summary>
public abstract class EventBase : MonoBehaviour
{
    protected InputAction _enter;
    protected Queue<Action> _event = new Queue<Action>();
    protected Action _currentEvent;
    protected int _eventIndex = 0;

    private void Start()
    {
        if (tag != "Event")
        {
            tag = "Event";
        }

        if (gameObject.layer != LayerMask.NameToLayer("Event"))
        {
            gameObject.layer = LayerMask.NameToLayer("Event");
        }

        _enter = InputSystem.actions.FindAction("Enter");

        EventSetting();
    }

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    public abstract void Event(PlayerInfo player);

    //if (_event.Count() > 0)
    //{
    //    if (_currentEvent == null)
    //    {
    //        _currentEvent = _event.Dequeue();
    //    }
    //}
    //if (_currentEvent != null) _currentEvent();

    /// <summary>イベントを設定する関数</summary>
    protected abstract void EventSetting();
}
