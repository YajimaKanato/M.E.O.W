using UnityEngine;
using System;
using Interface;
using System.Collections;

public class AndroidEvent : ConversationEventBase
{
    [SerializeField, TextArea] string[] _phase1Texts;
    protected override void EventSetting()
    {
        _eventEnumerator.Enqueue(Phase1Event);
    }

    IEnumerator Phase1Event(PlayerInfo player)
    {
        Debug.Log("EventStart");
        _interactUIManager.ConversationStart(this, player);
        _interactUIManager.MessageOpen();
        foreach (var phase in _phase1Texts)
        {
            _interactUIManager.MessageTextUpdate(phase);
            yield return null;
        }
        Debug.Log("Event End");
        _interactUIManager.ConversationEnd();
        _interactUIManager.MessageClose();
    }
}
