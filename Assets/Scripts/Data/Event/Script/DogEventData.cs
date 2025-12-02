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
        _gameManager.DataManager.ConversationRunTime.CharacterDataSetting(_gameManager.DataManager.PlayerRunTimeOnPlayScene, _gameManager.DataManager.DogEvent);
        _gameManager.UIManager.OpenConversation();
        foreach (var phase in _phase1Texts)
        {
            _gameManager.UIManager.MessageTextUpdate(phase, 0);
            _gameManager.UIManager.OpenMessage();
            yield return null;
            _gameManager.UIManager.UIClose();
        }
        Debug.Log("Event End");
        _gameManager.UIManager.UIClose();
    }

    /// <summary>
    /// フェーズ2のイベントフローを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator Phase2Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
        _gameManager.DataManager.ConversationRunTime.CharacterDataSetting(_gameManager.DataManager.PlayerRunTimeOnPlayScene, _gameManager.DataManager.DogEvent);
        _gameManager.UIManager.OpenConversation();
        for (int i = 0; i < _phase2Texts.Length - 1; i++)
        {
            _gameManager.UIManager.MessageTextUpdate(_phase2Texts[i], 0);
            _gameManager.UIManager.OpenMessage();
            yield return null;
            if (i < _phase2Texts.Length - 2) _gameManager.UIManager.UIClose();
        }
        _gameManager.GameActionManager.GetItem(_item);
        _gameManager.UIManager.OpenGetItem();
        yield return null;
        _gameManager.UIManager.UIClose();
        _gameManager.UIManager.UIClose();
        _gameManager.UIManager.MessageTextUpdate(_phase2Texts[_phase2Texts.Length - 1], 0);
        _gameManager.UIManager.OpenMessage();
        yield return null;
        Debug.Log("Event End");
        _gameManager.UIManager.UIClose();
        _gameManager.UIManager.UIClose();
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
        _gameManager.DataManager.ConversationRunTime.CharacterDataSetting(_gameManager.DataManager.PlayerRunTimeOnPlayScene, _gameManager.DataManager.DogEvent);
        _gameManager.UIManager.OpenConversation();
        foreach (var phase in _phase3Texts)
        {
            _gameManager.UIManager.MessageTextUpdate(phase, 0);
            _gameManager.UIManager.OpenMessage();
            yield return null;
            _gameManager.UIManager.UIClose();
        }
        Debug.Log("Event End");
        _gameManager.UIManager.UIClose();
    }
}
