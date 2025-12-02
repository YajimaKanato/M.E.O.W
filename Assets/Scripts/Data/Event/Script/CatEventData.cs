using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CatEvent", menuName = "Event/Conversation/CatEvent")]
public class CatEventData : ConversationEventBase
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
        _gameManager.DataManager.ConversationRunTime.CharacterDataSetting(_gameManager.DataManager.PlayerRunTimeOnPlayScene, _gameManager.DataManager.CatEvent);
        _gameManager.UIManager.OpenConversation();
        _gameManager.UIManager.OpenMessage();
        foreach (var phase in _phase1Texts)
        {
            _gameManager.UIManager.MessageTextUpdate(phase, 0);
            yield return null;
        }
        Debug.Log("Event End");
        _gameManager.UIManager.UIClose();
        _gameManager.UIManager.UIClose();
    }
}
