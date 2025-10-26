using UnityEngine;
using System;

public class CatEvent : EventBase
{
    protected override void EventSetting()
    {
        _event = new Action[] { () => Debug.Log("ƒjƒƒ[") };
    }
}
