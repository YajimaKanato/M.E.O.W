using UnityEngine;
using System.Collections;
using Interface;

public class DogEvent : EventBase, IConversationInteract
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    [SerializeField, TextArea] string[] _phase1Texts;
    [SerializeField, TextArea] string[] _phase2Texts;

    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;

    public void ConversationInteractStart(PlayerInfo player)
    {
        GameActionManager.ConversationInteract(this, player);
    }

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
        ConversationInteractStart(player);
        foreach (var phase in _phase1Texts)
        {
            StoryManager.TextUpdate(phase);
            yield return null;
        }
        Debug.Log("Event End");
    }
}
