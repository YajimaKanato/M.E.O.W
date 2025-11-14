using UnityEngine;
using System.Collections;
using System;

public class MouseEvent : CharacterEventBase
{
    [SerializeField, TextArea] string[] _phase1Texts;
    protected override void EventSetting()
    {
        _eventEnumerator.Enqueue(Phase1Event);
    }

    IEnumerator Phase1Event(PlayerInfo player)
    {
        Debug.Log("EventStart");
        ConversationInteractStart(player);
        foreach (var phase in _phase1Texts)
        {
            StoryManager.Instance.TextUpdate(phase);
            yield return null;
        }
        Debug.Log("Event End");
    }
}
