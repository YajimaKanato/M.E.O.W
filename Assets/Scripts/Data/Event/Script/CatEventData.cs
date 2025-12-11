using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatEvent", menuName = "Event/Conversation/CatEvent")]
public class CatEventData : EventBaseData
{
    [SerializeField, TextArea] string[] _phase1Texts;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        InitializeManager.InitializationForVariable(out _eventEnumerator, new Queue<Func<IEnumerator>>());
        if (!EventSetting()) InitializeManager.FailedInitialization();
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
        //_dataManager.ConversationSetting(RunTimeData.Player, RunTimeData.Cat);
        //_uiManager.OpenConversation();
        //_uiManager.OpenMessage();
        foreach (var phase in _phase1Texts)
        {
            _uiManager.MessageTextUpdate(phase, 0);
            yield return null;
        }
        Debug.Log("Event End");
        _uiManager.UIClose();
        _uiManager.UIClose();
    }
}

#region Cat
public class CatEventRunTime : EventRunTime, IRunTime
{
    CatEventData _catEventData;

    public CatEventRunTime(CatEventData data)
    {
        _catEventData = data;
        _eventEnumerator = _catEventData.EventEnumerator;
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
            if (_catEventData.IsNext)
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
