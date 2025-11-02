using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interface;

public abstract class EventBase : MonoBehaviour
{
    protected InputAction _enter;
    protected Queue<Action> _event;
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
    public void Event()
    {
        if (_event.Count() > 0)
        {
            if (_currentEvent == null)
            {
                _currentEvent = _event.Dequeue();
            }
            else
            {
                _currentEvent();
            }
        }
    }

    /// <summary>イベントを設定する関数</summary>
    protected abstract void EventSetting();
}
