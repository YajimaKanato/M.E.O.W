using UnityEngine;
using System.Collections;
using Interface;

public class AndroidEventRunTime : EventRunTime, IRunTime
{
    AndroidEventData _androidEventData;

    public AndroidEventRunTime(AndroidEventData data)
    {
        _androidEventData = data;
        _eventEnumerator = _androidEventData.EventEnumerator;
    }

    public override IEnumerator Event()
    {
        if (_eventEnumerator == null)
        {
            Debug.Log("Event Enumerator is null");
            return null;
        }

        //イベントが登録されている
        if (_eventEnumerator.Count > 0)
        {
            //現在行うイベントが登録されていない
            if (_androidEventData.IsNext)
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
