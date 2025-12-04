using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunTime;

[CreateAssetMenu(fileName = "DogEvent", menuName = "Event/Conversation/DogEvent")]
public class DogEventData : ConversationEventBase
{
    [SerializeField] UsableItem _item;
    [SerializeField, TextArea] string[] _phase1Texts;
    [SerializeField, TextArea] string[] _phase2Texts;
    [SerializeField, TextArea] string[] _phase3Texts;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        InitializeManager.InitializationForVariable(out _dataManager, _gameManager.DataManager);
        InitializeManager.InitializationForVariable(out _eventEnumerator, new Queue<Func<IEnumerator>>());
        if (!EventSetting()) InitializeManager.FailedInitialization();
        _isNext = true;
        return _isInitialized;
    }


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
        _dataManager.ConversationSetting(RunTimeData.Player, RunTimeData.Dog);
        _uiManager.OpenConversation();
        foreach (var phase in _phase1Texts)
        {
            _uiManager.MessageTextUpdate(phase, 0);
            _uiManager.OpenMessage();
            yield return null;
            _uiManager.UIClose();
        }
        Debug.Log("Event End");
        _uiManager.UIClose();
    }

    /// <summary>
    /// フェーズ2のイベントフローを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator Phase2Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
        _dataManager.ConversationSetting(RunTimeData.Player, RunTimeData.Dog);
        _uiManager.OpenConversation();
        for (int i = 0; i < _phase2Texts.Length - 1; i++)
        {
            _uiManager.MessageTextUpdate(_phase2Texts[i], 0);
            _uiManager.OpenMessage();
            yield return null;
            if (i < _phase2Texts.Length - 2) _uiManager.UIClose();
        }
        yield return _dataManager.GetItem(_item);
        _uiManager.UIClose();
        _uiManager.UIClose();
        _uiManager.MessageTextUpdate(_phase2Texts[_phase2Texts.Length - 1], 0);
        _uiManager.OpenMessage();
        yield return null;
        Debug.Log("Event End");
        _uiManager.UIClose();
        _uiManager.UIClose();
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
        _dataManager.ConversationSetting(RunTimeData.Player, RunTimeData.Dog);
        _uiManager.OpenConversation();
        foreach (var phase in _phase3Texts)
        {
            _uiManager.MessageTextUpdate(phase, 0);
            _uiManager.OpenMessage();
            yield return null;
            _uiManager.UIClose();
        }
        Debug.Log("Event End");
        _uiManager.UIClose();
    }
}
