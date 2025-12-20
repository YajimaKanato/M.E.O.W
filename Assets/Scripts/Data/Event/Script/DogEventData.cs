using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>犬のイベントデータ</summary>
[CreateAssetMenu(fileName = "DogEvent", menuName = "Event/Conversation/DogEvent")]
public class DogEventData : EventBaseData
{
    [SerializeField, Tooltip("与えるアイテム")] UsableItem _item;
    [SerializeField, Tooltip("会話のテキスト"), TextArea] string[] _phase1Texts;
    [SerializeField, Tooltip("会話のテキスト"), TextArea] string[] _phase2Texts;
    [SerializeField, Tooltip("会話のテキスト"), TextArea] string[] _phase3Texts;
    GameFlowManager _gameFlowManager;

    public override bool Init(GameManager manager)
    {
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _eventManager, _gameManager.EventManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _gameFlowManager, _gameManager.GameFlowManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _eventEnumerator, new Queue<Func<IEnumerator>>());
        if (!EventSetting()) _isInitialized = InitializeManager.FailedInitialization();
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
        _eventManager.StartConversation(_eventManager.Player, _eventManager.Dog);
        _eventManager.StartMessage(_phase1Texts[0], 0);
        yield return null;
        for (int i = 1; i < _phase1Texts.Length; i++)
        {
            _eventManager.MessageUpdate(_phase1Texts[i], 0);
            yield return null;
        }
        _uiManager.UIClose();
        Debug.Log("Event End");
    }

    /// <summary>
    /// フェーズ2のイベントフローを行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator Phase2Event()
    {
        Debug.Log("EventStart");
        _isNext = false;
        _eventManager.StartConversation(_eventManager.Player, _eventManager.Dog);
        _eventManager.StartMessage(_phase2Texts[0], 0);
        yield return null;
        for (int i = 1; i < _phase2Texts.Length - 1; i++)
        {
            _eventManager.MessageUpdate(_phase2Texts[i], 0);
            yield return null;
        }
        if (!_eventManager.GiveItem(_item))
        {
            yield return null;
            _uiManager.OpenItemChange(_item);
        }
        yield return null;
        _eventManager.MessageUpdate(_phase2Texts[_phase2Texts.Length - 1], 0);
        yield return null;
        _uiManager.UIClose();
        Debug.Log("Event End");
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
        _eventManager.StartConversation(_eventManager.Player, _eventManager.Dog);
        _eventManager.StartMessage(_phase3Texts[0], 0);
        yield return null;
        for (int i = 1; i < _phase3Texts.Length; i++)
        {
            _eventManager.MessageUpdate(_phase3Texts[i], 0);
            yield return null;
        }
        Debug.Log("Event End");
        _uiManager.UIClose();
    }
}

#region Dog
/// <summary>犬のランタイムデータ</summary>
public class DogEventRunTime : EventRunTime, IRunTime
{
    DogEventData _dogEventData;
    public DogEventRunTime(DogEventData data)
    {
        _dogEventData = data;
        _eventEnumerator = _dogEventData.EventEnumerator;
    }

    public override IEnumerator Event()
    {
        if (_eventEnumerator == null)
        {
            Debug.Log("Event Enumerator is null");
            return null;
        }

        //イベントが登録されている
        if (_eventEnumerator.Count > 0)
        {
            //現在行うイベントが登録されていない
            if (_dogEventData.IsNext)
            {
                _currentEnumerator = _eventEnumerator.Dequeue();
                Debug.Log("Event Dequeue");
            }
        }
        else
        {
            Debug.Log("There are no Events");
        }

        if (_currentEnumerator != null)
        {
            Debug.Log("Event Registering");
            return _currentEnumerator();
        }
        else
        {
            return null;
        }
    }
}
#endregion
