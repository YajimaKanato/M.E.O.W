using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>イベントのベースクラス</summary>
public abstract class EventBase : MonoBehaviour
{
    [SerializeField, Tooltip("インタラクト対象になったときの表示オブジェクト")] GameObject _targetSign;
    /// <summary>イベントを保存しておくキュー</summary>
    protected Queue<Func<PlayerInfo, IEnumerator>> _eventEnumerator = new Queue<Func<PlayerInfo, IEnumerator>>();
    /// <summary>現在行うイベント</summary>
    protected Func<PlayerInfo, IEnumerator> _currentEnumerator;

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
        _targetSign.SetActive(false);
        EventSetting();
    }

    /// <summary>
    /// インタラクト対象表示をアクティブにする関数
    /// </summary>
    public void TargetSignActive()
    {
        _targetSign.SetActive(true);
    }

    /// <summary>
    /// インタラクト対象表示を非アクティブにする関数
    /// </summary>
    public void TargetSignInactive()
    {
        _targetSign.SetActive(false);
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
