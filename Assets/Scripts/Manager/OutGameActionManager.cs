using Interface;
using Scene;
using Title;
using UnityEngine;

/// <summary>アウトゲームのアクションに関する制御を行うクラス</summary>
public class OutGameActionManager : InitializeBehaviour
{
    OutGameUIManager _outGameUIManager;
    DataManager _dataManager;
    TitleRunTime _titleRunTime;
    MenuRunTime _menuRunTime;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _outGameUIManager, _gameManager.OutGameUIManager);
        InitializeManager.InitializationForVariable(out _dataManager, _gameManager.DataManager);
        InitializeManager.InitializationForVariable(out _titleRunTime, _dataManager.TitleRunTime);
        InitializeManager.InitializationForVariable(out _menuRunTime, _dataManager.MenuRunTime);
        return _isInitialized;
    }

    #region アウトゲーム関連
    /// <summary>
    /// タイトルの操作を行う関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void TitleSelect(int index)
    {
        if (_outGameUIManager.ActionCheck<ISelectableVerticalArrowUI>())
        {
            _titleRunTime.SelectTitle(index);
            _outGameUIManager.Select<ISelectableVerticalArrowUI>();
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
        var init = true;
        switch (_titleRunTime.CurrentTitleIndex)
        {
            case (int)TitleCategory.Start:
                _gameManager.GameFlowManager.SceneChange(SceneName.Game.ToString());
                break;
            case (int)TitleCategory.EndingList:
                break;
            case (int)TitleCategory.Option:
                init = _outGameUIManager.OpenMenu();
                break;
            case (int)TitleCategory.Credit:
                init = _outGameUIManager.OpenCredit();
                break;
            case (int)TitleCategory.Reset:
                break;
            default:
                break;
        }
        if (!init) Debug.Log("Invaild Command");
    }

    /// <summary>
    /// メニューを選ぶ関数
    /// </summary>
    /// <param name="index">選んだスロットの番号</param>
    public void MenuSelectForKeyboard(int index)
    {
        if (_outGameUIManager.ActionCheck<ISelectableNumberUI>())
        {
            _menuRunTime.SelectMenuForKeyboard(index);
            _outGameUIManager.Select<ISelectableNumberUI>();
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
        if (_outGameUIManager.ActionCheck<ISelectableNumberUI>())
        {
            _menuRunTime.SelectMenuForGamepad(index);
            _outGameUIManager.Select<ISelectableNumberUI>();
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
