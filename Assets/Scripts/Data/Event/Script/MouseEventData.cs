using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MouseEvent", menuName = "Event/Conversation/MouseEvent")]
public class MouseEventData : ConversationEventBase
{
    [SerializeField, TextArea] string[] _phase1Texts;
    MouseEventRunTime _mouseEventRunTime;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        InitializeManager.InitializationForVariable(out _dataManager,_gameManager.DataManager);
        InitializeManager.InitializationForVariable(out _conversationRunTime,_dataManager.ConversationRunTime);
        InitializeManager.InitializationForVariable(out _playerRunTimeOnPlayScene,_dataManager.PlayerRunTimeOnPlayScene);
        InitializeManager.InitializationForVariable(out _mouseEventRunTime,_dataManager.MouseEvent);
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
        _conversationRunTime.CharacterDataSetting(_playerRunTimeOnPlayScene, _mouseEventRunTime);
        _uiManager.OpenConversation();
        _uiManager.OpenMessage();
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
