using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>イベントのベースクラス</summary>
public abstract class EventBaseData : InitializeObject
{
    /// <summary>イベントを保存しておくキュー</summary>
    protected Queue<Func<PlayerInfo, IEnumerator>> _eventEnumerator = new Queue<Func<PlayerInfo, IEnumerator>>();
    /// <summary>現在行うイベント</summary>
    protected Func<PlayerInfo, IEnumerator> _currentEnumerator;
    protected GameManager _initManager;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override void Init(GameManager manager)
    {
        EventSetting();
        _initManager = manager;
        Debug.Log($"{this} has Initialized");
    }

    /// <summary>
    /// イベントを次に進める関数
    /// </summary>
    protected void NextEvent()
    {
        _currentEnumerator = null;
    }

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
        else
        {
            Debug.Log("There are no Events");
        }

        if (_currentEnumerator != null)
        {
            Debug.Log("Event Registering");
            return _currentEnumerator(player);
        }
        else
        {
            return null;
        }
    }

    /// <summary>イベントを設定する関数</summary>
    protected abstract void EventSetting();
}
