using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AndroidEvent", menuName = "Event/Conversation/AndroidEvent")]
public class AndroidEventData : ConversationEventBase
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
        _gameManager.UIManager.ConversationSetting(_gameManager.DataManager.Player, this);
        _gameManager.UIManager.MessageOpen();
        foreach (var phase in _phase1Texts)
        {
            _gameManager.UIManager.MessageTextUpdate(phase);
            yield return null;
        }
        Debug.Log("Event End");
        _gameManager.UIManager.ConversationEnd();
        _gameManager.UIManager.MessageClose();
    }
}
