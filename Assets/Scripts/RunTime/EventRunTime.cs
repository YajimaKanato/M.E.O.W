using System;
using System.Collections;
using System.Collections.Generic;

public abstract class EventRunTime
{
    /// <summary>イベントを保存しておくキュー</summary>
    protected Queue<Func<IEnumerator>> _eventEnumerator;
    /// <summary>現在行うイベント</summary>
    protected Func<IEnumerator> _currentEnumerator;

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <returns>実行するイベント</returns>
    public abstract IEnumerator Event();
}
