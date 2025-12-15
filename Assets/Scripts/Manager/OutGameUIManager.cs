using Interface;
using Scene;
using System.Collections.Generic;
using Title;

/// <summary>アウトゲームのUIに関する制御を行うクラス</summary>
public class OutGameUIManager : UIManagerBase
{
    TitleUI _titleUI;
    CreditUI _creditUI;
    MenuUI _menuUI;

    public override bool Init(GameManager manager)
    {
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _uiStack, new Stack<IUIBase>());
        if (_uiStack != null) _uiStack.Push(null);

        foreach (var ui in _uiSettings)
        {
            if (ui.UI is TitleUI)
            {
                _isInitialized = InitializeManager.InitializationForVariable(out _titleUI, ui.UI as TitleUI);
            }
            else if (ui.UI is MenuUI)
            {
                _isInitialized = InitializeManager.InitializationForVariable(out _menuUI, ui.UI as MenuUI);
            }
            else if (ui.UI is CreditUI)
            {
                _isInitialized = InitializeManager.InitializationForVariable(out _creditUI, ui.UI as CreditUI);
            }
            ui.UI.Init(manager);
            if (ui.IsActive) _uiStack.Push((IUIBase)ui.UI);
            ui.UI.gameObject.SetActive(ui.IsActive);
        }

        return _isInitialized;
    }

    #region UIに関する詳細な処理
    /// <summary>
    /// タイトル画面でエンターを押したときの関数
    /// </summary>
    public void TitleEnter()
    {
        switch (_runtimeDataManager.GetData<TitleRunTime>(_titleUI.ID).CurrentTitleIndex)
        {
            case (int)TitleCategory.Start:
                _gameManager.GameFlowManager.SceneChange(SceneName.Game.ToString());
                break;
            case (int)TitleCategory.EndingList:
                break;
            case (int)TitleCategory.Option:
                OpenMenu();
                break;
            case (int)TitleCategory.Credit:
                OpenCredit();
                break;
            case (int)TitleCategory.Reset:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// メニューを開けるかを確認する関数
    /// </summary>
    public bool OpenMenu()
    {
        return OpenUI(_menuUI);
    }

    /// <summary>
    /// クレジットのUIを開けるかを確認する関数
    /// </summary>
    public bool OpenCredit()
    {
        return OpenUI(_creditUI);
    }
    #endregion
}
