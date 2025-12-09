using ActionMap;
using Interface;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ゲーム内のアクションに関する制御を行うクラス</summary>
public class GameActionManager : InitializeBehaviour
{
    ObjectManager _dataManager;
    UIManager _uiManager;
    PlayerInputActionManager _playerInputActionManager;
    HotbarRunTime _hotbarRunTime;
    MessageRunTime _messageRunTime;

    IEnumerator _eventEnumerator;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _dataManager, _gameManager.ObjectManager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        InitializeManager.InitializationForVariable(out _playerInputActionManager, _gameManager.PlayerInputActionManager);
        return _isInitialized;
    }

    #region アイテム関連
    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    /// <param name="id">アイテムを使用したキャラクターのID</param>
    public void ItemUse(int id)
    {
        var item = _uiManager.ItemUse();
        if (item != null)
        {
            item.ItemBaseActivate(id);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }
    #endregion

    #region インタラクト関連

    /// <summary>
    /// インタラクトを行う関数
    /// </summary>
    /// <returns>イベントの流れ</returns>
    public void Interact()
    {
        var target = _dataManager.Target;
        if (!target)
        {
            Debug.Log("Not Event");
            return;
        }

        if (_eventEnumerator == null)
        {
            _eventEnumerator = target.Event();
            if (_eventEnumerator == null) return;
            Debug.Log("Event Happened");
            _playerInputActionManager.ChangeActionMap(ActionMapName.UI);
            _eventEnumerator.MoveNext();
        }
        else
        {
            Debug.Log("Already Event Happened");
        }
    }

    /// <summary>
    /// エンター入力に対するアクションを行う関数
    /// </summary>
    public void PushEnter()
    {
        if (_uiManager.ActionCheck<IEnterUI>())
        {
            _uiManager.PushEnter();
            //イベント発生中
            if (_eventEnumerator != null)
            {
                if (!_uiManager.IsTyping())
                {
                    if (!_eventEnumerator.MoveNext())
                    {
                        _playerInputActionManager.ChangeActionMap(ActionMapName.Player);
                        _eventEnumerator = null;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Invaild Command");
        }
    }
    #endregion

    #region UI関連
    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選んだスロットの番号</param>
    public void ItemSelectForKeyboard(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUIForKeyboard>())
        {
            _uiManager.Select<ISelectableNumberUIForKeyboard>(index);
        }
        else
        {
            Debug.Log("Invaild Command");
        }
    }

    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void ItemSelectForGamepad(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUIForGamepad>())
        {
            _uiManager.Select<ISelectableNumberUIForGamepad>(index);
        }
        else
        {
            Debug.Log("Invaild Command");
        }
    }


    /// <summary>
    /// メニューを開く関数
    /// </summary>
    public void OpenMenu()
    {
        if (_uiManager.OpenMenu())
        {
            _playerInputActionManager.ChangeActionMap(ActionMapName.UI);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// メニューを選ぶ関数
    /// </summary>
    /// <param name="index">選んだスロットの番号</param>
    public void MenuSelectForKeyboard(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUIForKeyboard>())
        {
            _uiManager.Select<ISelectableNumberUIForKeyboard>(index);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// メニューを選ぶ関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void MenuSelectForGamepad(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUIForKeyboard>())
        {
            _uiManager.Select<ISelectableNumberUIForKeyboard>(index);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// UIを閉じる関数
    /// </summary>
    public void CloseUI()
    {
        if (_uiManager.CloseUI())
        {
            _playerInputActionManager.ChangeActionMap();
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }
    #endregion
}
