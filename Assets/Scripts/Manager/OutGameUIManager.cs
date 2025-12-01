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

    Stack<IUIBase> _uiStack;
    Stack<ISelectableVerticalArrowUI> _selectStack;
    Stack<IClosableUI> _closeStack;
    Stack<IOpenableUI> _openStack;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        _uiStack = new Stack<IUIBase>();
        _selectStack = new Stack<ISelectableVerticalArrowUI>();
        _closeStack = new Stack<IClosableUI>();
        _openStack = new Stack<IOpenableUI>();
        _uiStack.Push(null);
        _selectStack.Push(null);
        _closeStack.Push(null);
        _openStack.Push(null);

        foreach (var ui in _uiSettings)
        {
            if (ui.UI is TitleUI)
            {
                _titleUI = ui.UI as TitleUI;
                if (!_titleUI) FailedInitialization();
                //if (ui.IsActive) _openStack.Push(_titleUI);
            }
            else if (ui.UI is MenuUI)
            {
                _menuUI = ui.UI as MenuUI;
                if (!_menuUI) FailedInitialization();
                //if (ui.IsActive) _selectStack.Push(_menuUI);
            }
            else if (ui.UI is CreditUI)
            {
                _creditUI = ui.UI as CreditUI;
                if (!_creditUI) FailedInitialization();
            }
            ui.UI.Init(manager);
            if (ui.IsActive) _uiStack.Push((IUIBase)ui.UI);
            ui.UI.gameObject.SetActive(ui.IsActive);
        }

        return _isInitialized;
    }

    #region UIに関する基本処理
    ///// <summary>
    ///// 指定のUIを開く関数
    ///// </summary>
    ///// <param name="ui">開くUI</param>
    //public bool OpenUI<T>(T ui) where T : UIBehaviour
    //{
    //    if (!(_uiStack.Peek() is T))
    //    {
    //        PushStack(ui, _openStack);
    //        PushStack(ui, _closeStack);
    //        PushStack(ui, _selectStack);
    //        Debug.Log(_openStack.Count);
    //        Debug.Log(_closeStack.Count);
    //        Debug.Log(_selectStack.Count);
    //        ui.gameObject.SetActive(true);
    //        return true;
    //    }
    //    return false;
    //}

    /// <summary>
    /// 指定のUIを開く関数
    /// </summary>
    /// <param name="ui">開くUI</param>
    public bool OpenUI<T>(T ui) where T : UIBehaviour
    {
        if (!ui.gameObject.activeSelf)
        {
            _uiStack.Push((IUIBase)ui);
            ui.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    ///// <summary>
    ///// UIを閉じられるかを確認する関数
    ///// </summary>
    ///// <returns>UIを閉じたかどうか</returns>
    //public bool UIClose()
    //{
    //    if (_closeStack.Peek() != null)
    //    {
    //        CloseUI((UIBehaviour)_closeStack.Peek());
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    ///// <summary>
    ///// UIを閉じる関数
    ///// </summary>
    ///// <param name="ui">閉じるUI</param>
    //public void CloseUI(UIBehaviour ui)
    //{
    //    ui.gameObject.SetActive(false);
    //    PopStack(_openStack);
    //    PopStack(_closeStack);
    //    PopStack(_selectStack);
    //    Debug.Log(_openStack.Count);
    //    Debug.Log(_closeStack.Count);
    //    Debug.Log(_selectStack.Count);
    //}

    /// <summary>
    /// UIを閉じられるかを確認する関数
    /// </summary>
    /// <returns>UIを閉じたかどうか</returns>
    public bool UIClose()
    {
        if (_uiStack.Peek() is IClosableUI)
        {
            CloseUI((UIBehaviour)_uiStack.Peek());
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// UIを閉じる関数
    /// </summary>
    /// <param name="ui">閉じるUI</param>
    public void CloseUI(UIBehaviour ui)
    {
        _uiStack.Pop();
        ui.gameObject.SetActive(false);
    }

    /// <summary>
    /// 任意のスタックに登録する関数
    /// </summary>
    /// <typeparam name="T">登録するスタックの型</typeparam>
    /// <param name="ui">登録するUI</param>
    /// <param name="stack">スタック</param>
    public void PushStack<T>(UIBehaviour ui, Stack<T> stack) where T : IUIBase
    {
        if (ui is T)
        {
            stack.Push((T)(object)ui);
        }
        else
        {
            stack.Push(default);
        }
    }

    /// <summary>
    /// スタックからポップする関数
    /// </summary>
    /// <typeparam name="T">スタックの型</typeparam>
    /// <param name="stack">スタック</param>
    public void PopStack<T>(Stack<T> stack) where T : IUIBase
    {
        stack.Pop();
    }

    /// <summary>
    /// 横方向の入力で選択切り替えを行う関数
    /// </summary>
    public void SelectArrowHorizontal()
    {
        ((ISelectableHorizontalArrowUI)_uiStack.Peek()).SelectedCategory();
    }

    /// <summary>
    /// 縦方向の入力で選択切り替えを行う関数
    /// </summary>
    public void SelectArrowVertical()
    {
        ((ISelectableVerticalArrowUI)_uiStack.Peek()).SelectedCategory();
    }

    /// <summary>
    /// 番号で選択切り替えを行う関数
    /// </summary>
    public void SelectNumber()
    {
        ((ISelectableNumberUI)_uiStack.Peek()).SelectedCategory();
    }
    #endregion

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

    /// <summary>
    /// スロットの選択中を切り替える関数
    /// </summary>
    public bool VerticalArrowSelecteCheck()
    {
        return _uiStack.Peek() is ISelectableVerticalArrowUI;
    }

    /// <summary>
    /// スロットの選択中を切り替える関数
    /// </summary>
    public bool HorizontalArrowSelecteCheck()
    {
        return _uiStack.Peek() is ISelectableHorizontalArrowUI;
    }

    /// <summary>
    /// スロットの選択中を切り替える関数
    /// </summary>
    public bool NumberSelecteCheck()
    {
        return _uiStack.Peek() is ISelectableNumberUI;
    }

    /// <summary>
    /// タイトルの項目を選ぶ関数
    /// </summary>
    public bool TitleSelect()
    {
        return _uiStack.Peek() is TitleUI;
    }
    #endregion
}
