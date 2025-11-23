using UnityEngine;
using System.Collections;

public class AndroidEventRunTime : EventRunTime
{
    AndroidEventData _androidEventData;

    public AndroidEventRunTime(AndroidEventData data)
    {
        _androidEventData = data;
        _eventEnumerator = _androidEventData.EventEnumerator;
    }

    public override IEnumerator Event(PlayerInfo player)
    {
        //ƒCƒxƒ“ƒg‚ª“o˜^‚³‚ê‚Ä‚¢‚é
        if (_eventEnumerator.Count > 0)
        {
            //Œ»İs‚¤ƒCƒxƒ“ƒg‚ª“o˜^‚³‚ê‚Ä‚¢‚È‚¢
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
            return _currentEnumerator(player);
        }
        else
        {
            return null;
        }
    }
}
