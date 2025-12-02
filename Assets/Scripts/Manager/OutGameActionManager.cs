using Interface;
using Scene;
using Title;
using UnityEngine;

public class OutGameActionManager : InitializeBehaviour
{
    public override bool Init(GameManager manager)
    {
        Initialization(out _gameManager, manager);
        return _isInitialized;
    }

    #region アウトゲーム関連
    /// <summary>
    /// タイトルの操作を行う関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void TitleSelect(int index)
    {
        if (_gameManager.OutGameUIManager.ActionCheck<ISelectableVerticalArrowUI>())
        {
            _gameManager.DataManager.TitleRunTime.SelectTitle(index);
            _gameManager.OutGameUIManager.Select<ISelectableVerticalArrowUI>();
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
        switch (_gameManager.DataManager.TitleRunTime.CurrentTitleIndex)
        {
            case (int)TitleCategory.Start:
                _gameManager.GameFlowManager.SceneChange(SceneName.Game.ToString());
                break;
            case (int)TitleCategory.EndingList:
                break;
            case (int)TitleCategory.Option:
                init = _gameManager.OutGameUIManager.OpenMenu();
                break;
            case (int)TitleCategory.Credit:
                init = _gameManager.OutGameUIManager.OpenCredit();
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
        if (_gameManager.OutGameUIManager.ActionCheck<ISelectableNumberUI>())
        {
            _gameManager.DataManager.MenuRunTime.SelectMenuForKeyboard(index);
            _gameManager.OutGameUIManager.Select<ISelectableNumberUI>();
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
        if (_gameManager.OutGameUIManager.ActionCheck<ISelectableNumberUI>())
        {
            _gameManager.DataManager.MenuRunTime.SelectMenuForGamepad(index);
            _gameManager.OutGameUIManager.Select<ISelectableNumberUI>();
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
        if (!_gameManager.OutGameUIManager.CloseUI())
        {
            Debug.Log("Invalid Command");
        }
    }
    #endregion
}
