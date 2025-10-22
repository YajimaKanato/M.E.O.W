using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class EventBase : MonoBehaviour
{
    protected InputAction _enter;
    protected Action[] _event;
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
        _event[_eventIndex]();
    }

    /// <summary>イベントを設定する関数</summary>
    protected abstract void EventSetting();
}
