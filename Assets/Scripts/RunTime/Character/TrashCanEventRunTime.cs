using Interface;
using System.Collections;
using UnityEngine;

public class TrashCanEventRunTime : EventRunTime, IRunTime
{
    TrashCanEventData _trashCanEventData;

    public TrashCanEventRunTime(TrashCanEventData data)
    {
        _trashCanEventData = data;
        _eventEnumerator = _trashCanEventData.EventEnumerator;
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
            if (_trashCanEventData.IsNext)
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
