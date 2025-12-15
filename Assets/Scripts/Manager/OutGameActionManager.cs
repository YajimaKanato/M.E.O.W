using Interface;
using UnityEngine;

/// <summary>アウトゲームのアクションに関する制御を行うクラス</summary>
public class OutGameActionManager : InitializeBehaviour
{
    OutGameUIManager _outGameUIManager;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _outGameUIManager, _gameManager.OutGameUIManager);
        return _isInitialized;
    }

    #region アウトゲーム関連
    /// <summary>
    /// 縦方向のセレクトを行う関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void VerticalArrowSelect(int index)
    {
        if (_outGameUIManager.ActionCheck<ISelectableVerticalArrowUI>())
        {
            _outGameUIManager.Select<ISelectableVerticalArrowUI>(index);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// 横方向のセレクトを行う関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void HorizontalArrowSelect(int index)
    {
        if (_outGameUIManager.ActionCheck<ISelectableHorizontalArrowUI>())
        {
            _outGameUIManager.Select<ISelectableHorizontalArrowUI>(index);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// タイトル画面でエンターを押したときの関数
    /// </summary>
    public void PushEnter()
    {
        if (_outGameUIManager.ActionCheck<IEnterUI>())
        {
            _outGameUIManager.PushEnter();
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
        if (_outGameUIManager.ActionCheck<ISelectableNumberUIForKeyboard>())
        {
            _outGameUIManager.Select<ISelectableNumberUIForKeyboard>(index);
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
        if (_outGameUIManager.ActionCheck<ISelectableNumberUIForGamepad>())
        {
            _outGameUIManager.Select<ISelectableNumberUIForGamepad>(index);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// キャンセルを押したときに行う関数
    /// </summary>
    public void PushCansel()
    {
        if (!_outGameUIManager.CloseUI())
        {
            Debug.Log("Invalid Command");
        }
    }
    #endregion
}
