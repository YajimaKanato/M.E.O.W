using Title;
using UnityEngine;

public class OutGameActionManager : InitializeBehaviour
{
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        return _isInitialized;
    }

    #region アウトゲーム関連
    /// <summary>
    /// タイトルの操作を行う関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void TitleSelect(int index)
    {
        if (_gameManager.OutGameUIManager.VerticalArrowSelecteCheck())
        {
            _gameManager.DataManager.TitleRunTime.SelectTitle(index);
            _gameManager.OutGameUIManager.SelectArrowVertical();
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
        if (_gameManager.OutGameUIManager.NumberSelecteCheck())
        {
            _gameManager.DataManager.MenuRunTime.SelectMenuForKeyboard(index);
            _gameManager.OutGameUIManager.SelectNumber();
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
        if (_gameManager.OutGameUIManager.NumberSelecteCheck())
        {
            _gameManager.DataManager.MenuRunTime.SelectMenuForGamepad(index);
            _gameManager.OutGameUIManager.SelectNumber();
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
        if (!_gameManager.OutGameUIManager.UIClose())
        {
            Debug.Log("Invalid Command");
        }
    }
    #endregion
}
