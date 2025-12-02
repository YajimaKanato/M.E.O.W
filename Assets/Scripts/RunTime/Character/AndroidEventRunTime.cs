using UnityEngine;
using System.Collections;
using Interface;

public class AndroidEventRunTime : EventRunTime, IRunTime, ITalkable
{
    AndroidEventData _androidEventData;
    Sprite _sprite;
    string _name;
    public string CharacterName => _name;

    public Sprite CharacterImage => _sprite;

    public AndroidEventRunTime(AndroidEventData data)
    {
        _androidEventData = data;
        _eventEnumerator = _androidEventData.EventEnumerator;
        _sprite = _androidEventData.CharacterImage;
        _name = _androidEventData.CharacterName;
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
