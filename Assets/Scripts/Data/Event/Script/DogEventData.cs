using UnityEngine;
using System.Collections;
using Interface;

[CreateAssetMenu(fileName = "DogEvent", menuName = "Event/Conversation/DogEvent")]
public class DogEventData : ConversationEventBase, IGiveItemInteract
{
    [SerializeField] ItemInfo _item;
    [SerializeField, TextArea] string[] _phase1Texts;
    [SerializeField, TextArea] string[] _phase2Texts;
    [SerializeField, TextArea] string[] _phase3Texts;

    public IItemBase Item => _item.ItemBase();

    protected override void EventSetting()
    {
        _eventEnumerator.Enqueue(Phase1Event);
        _eventEnumerator.Enqueue(Phase2Event);
        _eventEnumerator.Enqueue(Phase3Event);
    }

    /// <summary>
    /// フェーズ１のイベントフローを行う関数
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
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
        NextEvent();
    }

    /// <summary>
    /// フェーズ2のイベントフローを行う関数
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator Phase2Event(PlayerInfo player)
    {
        Debug.Log("EventStart");
        _initManager.InteractUIManager.ConversationStart(this, player);
        _initManager.InteractUIManager.MessageOpen();
        for (int i = 0; i < _phase2Texts.Length - 1; i++)
        {
            _initManager.InteractUIManager.MessageTextUpdate(_phase2Texts[i]);
            yield return null;
        }
        _initManager.InteractUIManager.GetItemUIOpen(this);
        _initManager.GameActionManager.GiveItemInteract(this, player);
        yield return null;
        _initManager.InteractUIManager.GetItemUIClose();
        _initManager.InteractUIManager.MessageTextUpdate(_phase2Texts[_phase2Texts.Length - 1]);
        yield return null;
        Debug.Log("Event End");
        _initManager.InteractUIManager.ConversationEnd();
        _initManager.InteractUIManager.MessageClose();
        NextEvent();
    }

    /// <summary>
    /// フェーズ3のイベントフローを行う関数
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator Phase3Event(PlayerInfo player)
    {
        Debug.Log("EventStart");
        _initManager.InteractUIManager.ConversationStart(this, player);
        _initManager.InteractUIManager.MessageOpen();
        foreach (var phase in _phase3Texts)
        {
            _initManager.InteractUIManager.MessageTextUpdate(phase);
            yield return null;
        }
        Debug.Log("Event End");
        _initManager.InteractUIManager.ConversationEnd();
        _initManager.InteractUIManager.MessageClose();
    }
}
