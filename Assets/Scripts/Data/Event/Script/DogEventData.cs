using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DogEvent", menuName = "Event/Conversation/DogEvent")]
public class DogEventData : ConversationEventBase
{
    [SerializeField] UsableItem _item;
    [SerializeField, TextArea] string[] _phase1Texts;
    [SerializeField, TextArea] string[] _phase2Texts;
    [SerializeField, TextArea] string[] _phase3Texts;

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
    /// <returns></returns>
    IEnumerator Phase1Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
        _gameManager.UIManager.ConversationSetting(_gameManager.DataManager.PlayerOnPlayScene, this);
        _gameManager.UIManager.MessageOpen();
        foreach (var phase in _phase1Texts)
        {
            _gameManager.UIManager.MessageTextUpdate(phase, 0);
            yield return null;
        }
        Debug.Log("Event End");
        _gameManager.UIManager.ConversationEnd();
        _gameManager.UIManager.MessageClose();
        NextEvent();
    }

    /// <summary>
    /// フェーズ2のイベントフローを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator Phase2Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
        _gameManager.UIManager.ConversationSetting(_gameManager.DataManager.PlayerOnPlayScene, this);
        _gameManager.UIManager.MessageOpen();
        for (int i = 0; i < _phase2Texts.Length - 1; i++)
        {
            _gameManager.UIManager.MessageTextUpdate(_phase2Texts[i], 0);
            yield return null;
        }
        _gameManager.UIManager.GetItemUIOpen(_item);
        _gameManager.GameActionManager.GiveItemInteract(_item);
        yield return null;
        _gameManager.UIManager.GetItemUIClose();
        _gameManager.UIManager.MessageTextUpdate(_phase2Texts[_phase2Texts.Length - 1], 0);
        yield return null;
        Debug.Log("Event End");
        _gameManager.UIManager.ConversationEnd();
        _gameManager.UIManager.MessageClose();
        NextEvent();
    }

    /// <summary>
    /// フェーズ3のイベントフローを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator Phase3Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
        _gameManager.UIManager.ConversationSetting(_gameManager.DataManager.PlayerOnPlayScene, this);
        _gameManager.UIManager.MessageOpen();
        foreach (var phase in _phase3Texts)
        {
            _gameManager.UIManager.MessageTextUpdate(phase, 0);
            yield return null;
        }
        Debug.Log("Event End");
        _gameManager.UIManager.ConversationEnd();
        _gameManager.UIManager.MessageClose();
    }
}
