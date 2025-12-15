using ActionMap;
using Interface;
using Scene;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>UIマネージャーのベースクラス</summary>
public abstract class UIManagerBase : InitializeBehaviour
{
    [System.Serializable]
    protected class UISettings
    {
        [SerializeField, Tooltip("シーン上のUI")] UIBehaviour _ui;
        [SerializeField, Tooltip("シーン開始時にアクティブにするかどうか")] bool _isActive = true;

        public UIBehaviour UI => _ui;
        public bool IsActive => _isActive;
    }

    [SerializeField, Tooltip("シーン上のUI")] protected UISettings[] _uiSettings;
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
            //タイトル以外ではアクションマップを変更
            if (SceneManager.GetActiveScene().name != SceneName.Title.ToString()) _gameManager.PlayerInputActionManager.ChangeActionMap(ActionMapName.UI);
            //開いたUIをスタックに登録
            _uiStack.Push(ui);
            go.gameObject.SetActive(true);
            //開いたUIで処理
            ui.OpenSetting();
            //開いたUIの数を更新
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
            //一つUIを閉じる
            UIClose(1);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 今開いているUIがメニューかどうかを判定する関数
    /// </summary>
    /// <returns>今開いているUIがメニューかどうか</returns>
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
        //引数に何も指定されていなければすべてのUIを閉じる
        for (int i = 0; i < (count == 0 ? closeCount : 1); i++)
        {
            //タイトル以外ではアクションマップを変更
            if (SceneManager.GetActiveScene().name != SceneName.Title.ToString()) _gameManager.PlayerInputActionManager.ChangeActionMap();
            //スタックからUIを押し出す
            var ui = _uiStack.Pop();
            //閉じたUIで処理
            ((IUIOpenAndClose)ui).Close();
            ((UIBehaviour)ui).gameObject.SetActive(false);
            //開いたUIの数を更新
            _openUICount--;
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
