using Interface;
using System.Collections;
using UnityEngine;

public class CatEventRunTime : EventRunTime, IRunTime, ITalkable
{
    CatEventData _catEventData;
    Sprite _sprite;
    string _name;
    public string CharacterName => _name;

    public Sprite CharacterImage => _sprite;

    public CatEventRunTime(CatEventData data)
    {
        _catEventData = data;
        _eventEnumerator = _catEventData.EventEnumerator;
        _sprite = _catEventData.CharacterImage;
        _name = _catEventData.CharacterName;
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
