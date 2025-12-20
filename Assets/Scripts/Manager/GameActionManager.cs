using Interface;
using System.Collections;
using UnityEngine;

/// <summary>ゲーム内のアクションに関する制御を行うクラス</summary>
public class GameActionManager : ManagerBase
{
    ObjectManager _dataManager;
    UIManager _uiManager;

    IEnumerator _eventEnumerator;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _dataManager, _gameManager.ObjectManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);

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

        //ターゲットからイベントを受け取る
        if (_eventEnumerator == null)
        {
            _eventEnumerator = target.Event();
            if (_eventEnumerator == null) return;
            Debug.Log("Event Happened");
            _eventEnumerator.MoveNext();
        }
        else
        {
            //ここは基本は通らない想定
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
    /// 番号で項目を選ぶ関数
    /// </summary>
    /// <param name="index">選んだスロットの番号</param>
    public void SelectForKeyboard(int index)
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
    /// 番号で項目を選ぶ関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void SelectForGamepad(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUIForGamepad>())
        {
            _uiManager.Select<ISelectableNumberUIForGamepad>(index);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// 横方向の矢印入力を行う関数
    /// </summary>
    /// <param name="index">入力方向</param>
    public void SelectHorizontalArrow(int index)
    {
        if (_uiManager.ActionCheck<ISelectableHorizontalArrowUI>())
        {
            _uiManager.Select<ISelectableHorizontalArrowUI>(index);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// 縦方向の矢印入力を行う関数
    /// </summary>
    /// <param name="index">入力方向</param>
    public void SelectVerticalArrow(int index)
    {
        if (_uiManager.ActionCheck<ISelectableVerticalArrowUI>())
        {
            _uiManager.Select<ISelectableVerticalArrowUI>(index);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// メニューを開く関数
    /// </summary>
    public void OpenMenu()
    {
        if (!_uiManager.OpenMenu())
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// UIを閉じる関数
    /// </summary>
    public void CloseUI()
    {
        if (_uiManager.IsMenu())
        {
            if (!_uiManager.CloseUI())
            {
                Debug.Log("Invalid Command");
            }
        }
        else
        {
            if (_uiManager.CloseUI())
            {
                if (_eventEnumerator != null)
                {
                    if (!_eventEnumerator.MoveNext())
                    {
                        _eventEnumerator = null;
                    }
                }
            }
            else
            {
                Debug.Log("Invalid Command");
            }
        }
    }
    #endregion
}
