using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MouseEvent", menuName = "Event/Conversation/MouseEvent")]
public class MouseEventData : ConversationEventBase
{
    [SerializeField, TextArea] string[] _phase1Texts;
    protected override void EventSetting()
    {
        _eventEnumerator.Enqueue(Phase1Event);
    }

    IEnumerator Phase1Event()
    {
        Debug.Log("EventStart");
        _gameManager.InteractUIManager.ConversationStart(this);
        _gameManager.InteractUIManager.MessageOpen();
        foreach (var phase in _phase1Texts)
        {
            _gameManager.InteractUIManager.MessageTextUpdate(phase);
            yield return null;
        }
        Debug.Log("Event End");
        _gameManager.InteractUIManager.ConversationEnd();
        _gameManager.InteractUIManager.MessageClose();
    }
}
