using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MouseEvent", menuName = "Event/Conversation/MouseEvent")]
public class MouseEventData : ConversationEventBase
{
    [SerializeField, TextArea] string[] _phase1Texts;
    protected override bool EventSetting()
    {
        _eventEnumerator.Enqueue(Phase1Event);
        return _eventEnumerator.Count > 0;
    }

    IEnumerator Phase1Event()
    {
        Debug.Log("EventStart");
        _gameManager.InteractUIManager.ConversationSetting(_gameManager.StatusManager.Player, this);
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
