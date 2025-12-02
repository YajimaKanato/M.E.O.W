using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>イベントのベースクラス</summary>
public abstract class EventBaseData : InitializeSO
{
    /// <summary>イベントを保存しておくキュー</summary>
    protected Queue<Func<IEnumerator>> _eventEnumerator;
    public Queue<Func<IEnumerator>> EventEnumerator => _eventEnumerator;

    protected bool _isNext;
    public bool IsNext => _isNext;

    /// <summary>
    /// イベントを次に進める関数
    /// </summary>
    protected void NextEvent()
    {
        _isNext = true;
    }

    /// <summary>イベントを設定する関数</summary>
    /// <returns>イベントを設定できたかどうか</returns>
    protected abstract bool EventSetting();
}
