using UnityEngine;
using System;
using Interface;

public class AndroidEvent : EventBase, ITalkInteract
{
    public override void Event(PlayerInfo player)
    {

    }

    protected override void EventSetting()
    {
        _event.Enqueue(() => Debug.Log("ÉKÉKÉK"));
    }
}
