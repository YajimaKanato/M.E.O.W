using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AndroidEvent", menuName = "Event/Conversation/AndroidEvent")]
public class AndroidEventData : ConversationEventBase
{
    [SerializeField, TextArea] string[] _phase1Texts;
    protected override void EventSetting()
    {
        _eventEnumerator.Enqueue(Phase1Event);
    }

    IEnumerator Phase1Event(PlayerInfo player)
    {
        Debug.Log("EventStart");
        _initManager.InteractUIManager.ConversationStart(this, player);
        _initManager.InteractUIManager.MessageOpen();
        foreach (var phase in _phase1Texts)
        {
            _initManager.InteractUIManager.MessageTextUpdate(phase);
            yield return null;
        }
        Debug.Log("Event End");
        _initManager.InteractUIManager.ConversationEnd();
        _initManager.InteractUIManager.MessageClose();
    }
}
