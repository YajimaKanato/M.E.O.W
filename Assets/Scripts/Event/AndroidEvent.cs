using UnityEngine;
using System;

public class AndroidEvent : EventBase
{
    protected override void EventSetting()
    {
        _event.Enqueue(() => Debug.Log("ÉKÉKÉK"));
    }
}
