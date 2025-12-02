using Interface;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIManagerBase : InitializeBehaviour
{
    [System.Serializable]
    protected class UISettings
    {
        [SerializeField] UIBehaviour _ui;
        [SerializeField] bool _isActive = true;

        public UIBehaviour UI => _ui;
        public bool IsActive => _isActive;
    }

    [SerializeField] protected UISettings[] _uiSettings;
    protected Stack<IUIBase> _uiStack = new Stack<IUIBase>();

    #region UIに関する基本処理
    /// <summary>
    /// 指定のUIを開く関数
    /// </summary>
    /// <param name="ui">開くUI</param>
    /// <returns>UIを開けたかどうか</returns>
    public bool OpenUI(IUIOpenAndClose ui)
    {
        var go = ui as UIBehaviour;
        if (!go.gameObject.activeSelf)
        {
            _uiStack.Push(ui);
            go.gameObject.SetActive(true);
            ui.OpenSetting();
            return true;
        }
        return false;
    }

    /// <summary>
    /// UIを閉じられるかを確認する関数
    /// </summary>
    /// <returns>UIを閉じたかどうか</returns>
    public bool CloseUI()
    {
        if (_uiStack.Peek() is IClosableUI)
        {
            UIClose();
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
    public void UIClose()
    {
        var ui = _uiStack.Pop();
        ((IUIOpenAndClose)ui).Close();
        ((UIBehaviour)ui).gameObject.SetActive(false);
    }

    /// <summary>
    /// UIの切り替えを行う関数
    /// </summary>
    /// <typeparam name="T">切り替えを行うアクションの種類</typeparam>
    public void Select<T>() where T : ISelectableUI
    {
        ((T)_uiStack.Peek()).SelectedCategory();
    }

    /// <summary>
    /// アクションが行えるかどうかを確認する関数
    /// </summary>
    /// <typeparam name="T">アクションの種類</typeparam>
    /// <returns>アクションが行えるかどうか</returns>
    public bool ActionCheck<T>() where T : IUIBase
    {
        return _uiStack.Peek() is T;
    }

    /// <summary>
    /// エンターを押したときの処理を行う関数
    /// </summary>
    public void PushEnter()
    {
        ((IEnterUI)_uiStack.Peek()).PushEnter();
    }
    #endregion
}
