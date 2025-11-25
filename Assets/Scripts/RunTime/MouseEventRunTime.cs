using System.Collections;
using UnityEngine;

public class MouseEventRunTime : EventRunTime
{
    MouseEventData _mouseEventData;

    public MouseEventRunTime(MouseEventData data)
    {
        _mouseEventData = data;
        _eventEnumerator = _mouseEventData.EventEnumerator;
    }

    public override IEnumerator Event()
    {
        //イベントが登録されている
        if (_eventEnumerator.Count > 0)
        {
            //現在行うイベントが登録されていない
            if (_mouseEventData.IsNext)
            {
                _currentEnumerator = _eventEnumerator.Dequeue();
            }
        }
        else
        {
            Debug.Log("There are no Events");
        }

        if (_currentEnumerator != null)
        {
            Debug.Log("Event Registering");
            return _currentEnumerator();
        }
        else
        {
            return null;
        }
    }
}
