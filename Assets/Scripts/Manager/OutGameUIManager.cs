using System.Collections.Generic;
using UnityEngine;
using Interface;

public class OutGameUIManager : InitializeBehaviour
{
    [System.Serializable]
    class UISettings
    {
        [SerializeField] UIBehaviour _ui;
        [SerializeField] bool _isActive = true;

        public UIBehaviour UI => _ui;
        public bool IsActive => _isActive;
    }

    [SerializeField] UISettings[] _uiSettings;
    TitleUI _titleUI;
    MenuUI _menuUI;
    CreditUI _creditUI;

    Stack<ISelectable> _selectStack;
    Stack<IClosableUI> _closeStack;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        _selectStack = new Stack<ISelectable>();
        _closeStack = new Stack<IClosableUI>();
        _selectStack.Push(null);
        _closeStack.Push(null);

        foreach (var ui in _uiSettings)
        {
            if (ui.UI is TitleUI)
            {
                _titleUI = ui.UI as TitleUI;
                if (!_titleUI) FailedInitialization();
            }
            else if (ui.UI is MenuUI)
            {
                _menuUI = ui.UI as MenuUI;
                if (!_menuUI) FailedInitialization();
                if (ui.IsActive) _selectStack.Push(_menuUI);
            }
            else if (ui.UI is CreditUI)
            {
                _creditUI = ui.UI as CreditUI;
                if (!_creditUI) FailedInitialization();
            }
            ui.UI.Init(manager);
            ui.UI.gameObject.SetActive(ui.IsActive);
        }

        return _isInitialized;
    }

    /// <summary>
    /// 指定のUIを開く関数
    /// </summary>
    /// <param name="ui">開くUI</param>
    public void OpenUI(UIBehaviour ui)
    {
        NextClosableUI(ui is IClosableUI ? (IClosableUI)ui : null);
        NextSelectableUI(ui is ISelectable ? (ISelectable)ui : null);
        ui.gameObject.SetActive(true);
    }

    /// <summary>
    /// メニューを開く関数
    /// </summary>
    public void OpenMenu()
    {
        OpenUI(_menuUI);
    }

    /// <summary>
    /// クレジットのUIを開く関数
    /// </summary>
    public void OpenCredit()
    {
        OpenUI(_creditUI);
    }

    /// <summary>
    /// タイトルの項目を選ぶ関数
    /// </summary>
    public void TitleSelect()
    {
        _titleUI.SelectedSlot();
    }

    /// <summary>
    /// セレクト可能UIを切り替える関数
    /// </summary>
    /// <param name="select">セレクト可能なUI</param>
    public void NextSelectableUI(ISelectable select)
    {
        _selectStack.Push(select);
    }

    /// <summary>
    /// セレクト可能UIを戻す関数
    /// </summary>
    public void ReturnSelectableUI()
    {
        if (_selectStack.Count > 0)
        {
            _selectStack.Pop();
        }
    }

    /// <summary>
    /// 閉じることのできるUIを切り替える関数
    /// </summary>
    /// <param name="close">閉じることのできるUI</param>
    public void NextClosableUI(IClosableUI close)
    {
        _closeStack.Push(close);
    }

    /// <summary>
    /// 閉じることのできるUIを戻す関数
    /// </summary>
    public UIBehaviour ReturnClosableUI()
    {
        if (_closeStack.Count > 0)
        {
            return (UIBehaviour)_closeStack.Pop();
        }

        return null;
    }

    /// <summary>
    /// スロットの選択中を切り替える関数
    /// </summary>
    public void SelectedSlot()
    {
        _selectStack.Peek()?.SelectedSlot();
    }

    /// <summary>
    /// UIを閉じる関数
    /// </summary>
    public void CloseUI()
    {
        ReturnClosableUI().gameObject.SetActive(false);
        ReturnSelectableUI();
    }
}
