using System.Collections;
using UnityEngine;

public class CatEventRunTime : EventRunTime
{
    CatEventData _catEventData;

    public CatEventRunTime(CatEventData data)
    {
        _catEventData = data;
        _eventEnumerator = _catEventData.EventEnumerator;
    }

    public override IEnumerator Event()
    {
        //イベントが登録されている
        if (_eventEnumerator.Count > 0)
        {
            //現在行うイベントが登録されていない
            if (_catEventData.IsNext)
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
