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

    public override IEnumerator Event(PlayerInfo player)
    {
        //ƒCƒxƒ“ƒg‚ª“o˜^‚³‚ê‚Ä‚¢‚é
        if (_eventEnumerator.Count > 0)
        {
            //Œ»İs‚¤ƒCƒxƒ“ƒg‚ª“o˜^‚³‚ê‚Ä‚¢‚È‚¢
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
            return _currentEnumerator(player);
        }
        else
        {
            return null;
        }
    }
}
