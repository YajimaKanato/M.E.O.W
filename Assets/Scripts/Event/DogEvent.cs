using UnityEngine;
using System.Collections;
using Interface;

public class DogEvent : ConversationEventBase
{
    [SerializeField, TextArea] string[] _phase1Texts;
    [SerializeField, TextArea] string[] _phase2Texts;

    protected override void EventSetting()
    {
        _eventEnumerator.Enqueue(Phase1Event);
    }

    /// <summary>
    /// フェーズ１のイベントフローを行う関数
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
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
