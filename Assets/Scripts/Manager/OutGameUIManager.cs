using System.Collections.Generic;
using Interface;

public class OutGameUIManager : UIManagerBase
{
    TitleUI _titleUI;
    MenuUI _menuUI;
    CreditUI _creditUI;

    public override bool Init(GameManager manager)
    {
        Initialization(out _gameManager, manager);
        Initialization(out _uiStack, new Stack<IUIBase>());
        _uiStack.Push(null);

        foreach (var ui in _uiSettings)
        {
            if (ui.UI is TitleUI)
            {
                Initialization(out _titleUI, ui.UI as TitleUI);
            }
            else if (ui.UI is MenuUI)
            {
                Initialization(out _menuUI, ui.UI as MenuUI);
            }
            else if (ui.UI is CreditUI)
            {
                Initialization(out _creditUI, ui.UI as CreditUI);
            }
            ui.UI.Init(manager);
            if (ui.IsActive) _uiStack.Push((IUIBase)ui.UI);
            ui.UI.gameObject.SetActive(ui.IsActive);
        }

        return _isInitialized;
    }

    #region UIに関する詳細な処理
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
