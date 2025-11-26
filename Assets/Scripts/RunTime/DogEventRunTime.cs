using System.Collections;
using UnityEngine;

public class DogEventRunTime : EventRunTime
{
    DogEventData _dogEventData;

    public DogEventRunTime(DogEventData data)
    {
        _dogEventData = data;
        _eventEnumerator = _dogEventData.EventEnumerator;
    }

    public override IEnumerator Event()
    {
        if(_eventEnumerator == null)
        {
            Debug.Log("Event Enumerator is null");
            return null;
        }

        //イベントが登録されている
        if (_eventEnumerator.Count > 0)
        {
            //現在行うイベントが登録されていない
            if (_dogEventData.IsNext)
            {
                _currentEnumerator = _eventEnumerator.Dequeue();
                Debug.Log("Event Dequeue");
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
