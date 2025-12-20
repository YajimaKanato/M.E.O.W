using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>アンドロイドのイベントデータ</summary>
[CreateAssetMenu(fileName = "AndroidEvent", menuName = "Event/Conversation/AndroidEvent")]
public class AndroidEventData : EventBaseData
{
    [SerializeField, Tooltip("会話のテキスト"), TextArea] string[] _phase1Texts;
    GameFlowManager _gameFlowManager;

    public override bool Init(GameManager manager)
    {
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
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
        return _eventEnumerator.Count > 0;
    }

    IEnumerator Phase1Event()
    {
        Debug.Log("EventStart");
        //_dataManager.ConversationSetting(RunTimeData.Player, RunTimeData.Android);
        //_uiManager.OpenConversation();
        //_uiManager.OpenMessage();
        foreach (var phase in _phase1Texts)
        {
            _uiManager.MessageTextUpdate(phase, 0);
            yield return null;
        }
        Debug.Log("Event End");
        _uiManager.CloseUI();
        _uiManager.CloseUI();
    }
}

#region Android
/// <summary>アンドロイドのランタイムデータ</summary>
public class AndroidEventRunTime : EventRunTime, IRunTime
{
    AndroidEventData _androidEventData;

    public AndroidEventRunTime(AndroidEventData data)
    {
        _androidEventData = data;
        _eventEnumerator = _androidEventData.EventEnumerator;
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
            if (_androidEventData.IsNext)
            {
                _currentEnumerator = _eventEnumerator.Dequeue();
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
