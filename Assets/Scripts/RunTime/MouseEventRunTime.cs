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
        //ƒCƒxƒ“ƒg‚ª“o˜^‚³‚ê‚Ä‚¢‚é
        if (_eventEnumerator.Count > 0)
        {
            //Œ»İs‚¤ƒCƒxƒ“ƒg‚ª“o˜^‚³‚ê‚Ä‚¢‚È‚¢
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
