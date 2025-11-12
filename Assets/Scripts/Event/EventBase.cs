using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>イベントのベースクラス</summary>
public abstract class EventBase : MonoBehaviour
{
    protected InputAction _enter;
    /// <summary>イベントを保存しておくキュー</summary>
    protected Queue<Action<PlayerInfo>> _event = new Queue<Action<PlayerInfo>>();
    /// <summary>現在行うイベント</summary>
    protected Action<PlayerInfo> _currentEvent;
    /// <summary>イベントを保存しておくキュー</summary>
    protected Queue<Func<PlayerInfo, IEnumerator>> _eventEnumerator = new Queue<Func<PlayerInfo, IEnumerator>>();
    /// <summary>現在行うイベント</summary>
    protected Func<PlayerInfo, IEnumerator> _currentEnumerator;

    /// <summary>ストーリーのマネージャークラス</summary>
    protected StoryManager _storyManager;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    protected virtual void Init()
    {
        if (tag != "Event")
        {
            tag = "Event";
        }

        if (gameObject.layer != LayerMask.NameToLayer("Event"))
        {
            gameObject.layer = LayerMask.NameToLayer("Event");
        }

        _enter = InputSystem.actions.FindAction("Enter");
        _storyManager = FindFirstObjectByType<StoryManager>();
        EventSetting();
    }

    ///// <summary>
    ///// イベントを起こす関数
    ///// </summary>
    ///// <param name="player">プレイヤーの情報</param>
    //public void Event(PlayerInfo player)
    //{
    //    //イベントが登録されている
    //    if (_event.Count > 0)
    //    {
    //        //現在行うイベントが登録されていない
    //        if (_currentEvent == null)
    //        {
    //            _currentEvent = _event.Dequeue();
    //        }
    //    }
    //    //イベント実行
    //    if (_currentEvent != null) _currentEvent(player);
    //}

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    /// <returns>実行するイベント</returns>
    public IEnumerator Event(PlayerInfo player)
    {
        //イベントが登録されている
        if (_eventEnumerator.Count > 0)
        {
            //現在行うイベントが登録されていない
            if (_currentEnumerator == null)
            {
                _currentEnumerator = _eventEnumerator.Dequeue();
            }
        }

        if (_currentEnumerator != null) return _currentEnumerator(player);
        else return null;
    }

    /// <summary>イベントを設定する関数</summary>
    protected abstract void EventSetting();
}
