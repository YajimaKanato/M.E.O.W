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

    protected override bool EventSetting()
    {
        _eventEnumerator.Enqueue(Phase1Event);
        _eventEnumerator.Enqueue(Phase2Event);
        _eventEnumerator.Enqueue(Phase3Event);
        return _eventEnumerator.Count > 0;
    }

    /// <summary>
    /// フェーズ１のイベントフローを行う関数
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator Phase1Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
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
        NextEvent();
    }

    /// <summary>
    /// フェーズ2のイベントフローを行う関数
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator Phase2Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
        _gameManager.InteractUIManager.ConversationStart(this);
        _gameManager.InteractUIManager.MessageOpen();
        for (int i = 0; i < _phase2Texts.Length - 1; i++)
        {
            _gameManager.InteractUIManager.MessageTextUpdate(_phase2Texts[i]);
            yield return null;
        }
        _gameManager.InteractUIManager.GetItemUIOpen(this);
        _gameManager.GameActionManager.GiveItemInteract(this);
        yield return null;
        _gameManager.InteractUIManager.GetItemUIClose();
        _gameManager.InteractUIManager.MessageTextUpdate(_phase2Texts[_phase2Texts.Length - 1]);
        yield return null;
        Debug.Log("Event End");
        _gameManager.InteractUIManager.ConversationEnd();
        _gameManager.InteractUIManager.MessageClose();
        NextEvent();
    }

    /// <summary>
    /// フェーズ3のイベントフローを行う関数
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator Phase3Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
        _gameManager.InteractUIManager.ConversationStart(this);
        _gameManager.InteractUIManager.MessageOpen();
        foreach (var phase in _phase3Texts)
        {
            _gameManager.InteractUIManager.MessageTextUpdate(phase);
            yield return null;
        }
        Debug.Log("Event End");
        _gameManager.InteractUIManager.ConversationEnd();
        _gameManager.InteractUIManager.MessageClose();
    }
}
