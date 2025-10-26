using UnityEngine;
using System;

public class AndroidEvent : EventBase
{
    protected override void EventSetting()
    {
        _event = new Action[] { () => Debug.Log("ÉKÉKÉK") };
    }
}
