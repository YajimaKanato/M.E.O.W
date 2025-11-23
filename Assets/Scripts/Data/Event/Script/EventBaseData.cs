using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>イベントのベースクラス</summary>
public abstract class EventBaseData : InitializeObject
{
    /// <summary>イベントを保存しておくキュー</summary>
    protected Queue<Func<PlayerInfo, IEnumerator>> _eventEnumerator = new Queue<Func<PlayerInfo, IEnumerator>>();
    public Queue<Func<PlayerInfo, IEnumerator>> EventEnumerator => _eventEnumerator;

    protected bool _isNext;
    public bool IsNext => _isNext;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override void Init(GameManager manager)
    {
        EventSetting();
        _gameManager = manager;
        Debug.Log($"{this} has Initialized");
    }

    /// <summary>
    /// イベントを次に進める関数
    /// </summary>
    protected void NextEvent()
    {
        _isNext = true;
    }

    /// <summary>イベントを設定する関数</summary>
    protected abstract void EventSetting();
}
