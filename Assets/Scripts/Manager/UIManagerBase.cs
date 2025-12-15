using ActionMap;
using Interface;
using System.Collections.Generic;
using UnityEngine;

/// <summary>UIマネージャーのベースクラス</summary>
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
    int _openUICount = 0;

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
            _gameManager.PlayerInputActionManager.ChangeActionMap(ActionMapName.UI);
            _uiStack.Push(ui);
            go.gameObject.SetActive(true);
            ui.OpenSetting();
            _openUICount++;
            Debug.Log(_openUICount);
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
            UIClose(1);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMenu()
    {
        return _uiStack.Peek() is MenuUI;
    }

    /// <summary>
    /// UIを閉じる関数
    /// </summary>
    /// <param name="count">閉じるUIの数</param>
    public void UIClose(int count = 0)
    {
        var closeCount = _openUICount;
        for (int i = 0; i < (count == 0 ? closeCount : 1); i++)
        {
            var ui = _uiStack.Pop();
            ((IUIOpenAndClose)ui).Close();
            ((UIBehaviour)ui).gameObject.SetActive(false);
            _openUICount--;
            _gameManager.PlayerInputActionManager.ChangeActionMap();
            Debug.Log(_openUICount);
        }
    }

    /// <summary>
    /// UIの切り替えを行う関数
    /// </summary>
    /// <typeparam name="T">切り替えを行うアクションの種類</typeparam>
    /// <param name="index">切り替えるインデックス</param>
    public void Select<T>(int index) where T : ISelectableUI
    {
        if (typeof(ISelectableHorizontalArrowUI).IsAssignableFrom(typeof(T)))
        {
            ((ISelectableHorizontalArrowUI)_uiStack.Peek()).SelectedCategory(index);
        }
        else if (typeof(ISelectableVerticalArrowUI).IsAssignableFrom(typeof(T)))
        {
            ((ISelectableVerticalArrowUI)_uiStack.Peek()).SelectedCategory(index);
        }
        else if (typeof(ISelectableNumberUIForGamepad).IsAssignableFrom(typeof(T)))
        {
            ((ISelectableNumberUIForGamepad)_uiStack.Peek()).SelectedCategory(index);
        }
        else if (typeof(ISelectableNumberUIForKeyboard).IsAssignableFrom(typeof(T)))
        {
            ((ISelectableNumberUIForKeyboard)_uiStack.Peek()).SelectedCategory(index);
        }
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
