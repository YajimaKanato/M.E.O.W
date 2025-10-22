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
    /// �C�x���g���N�����֐�
    /// </summary>
    public void Event()
    {
        _event[_eventIndex]();
    }

    /// <summary>�C�x���g��ݒ肷��֐�</summary>
    protected abstract void EventSetting();
}
