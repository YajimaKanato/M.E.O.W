using UnityEngine;
using System;

public class MouseEvent : EventBase
{
    public override void Event(PlayerInfo player)
    {

    }

    protected override void EventSetting()
    {
        _event.Enqueue(() => Debug.Log("ƒ`ƒ…["));
    }
}
