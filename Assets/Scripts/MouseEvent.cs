using UnityEngine;
using System;

public class MouseEvent : EventBase
{
    protected override void EventSetting()
    {
        _event = new Action[] { () => Debug.Log("ƒ`ƒ…[") };
    }
}
