using Interface;
using UnityEngine;

/// <summary>メニューの初期データ</summary>
[CreateAssetMenu(fileName = "MenuData", menuName = "UIData/MenuData")]
public class MenuData : InitializeSO
{
    [SerializeField, Tooltip("メニューの項目数")] int _menuCount = 4;
    [SerializeField, Tooltip("初期状態で選んでいるメニューの項目番号")] int _defaultSelectIndex = 0;
    public int MenuCount => _menuCount;
    public int DefaultSelectIndex => _defaultSelectIndex;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

#region Menu
/// <summary>メニューのランタイムデータ</summary>
public class MenuRunTime : IRunTime
{
    MenuData _menuData;
    int _menuIndex;
    int _currentMenuIndex = 0;

    public int MenuIndex => _menuIndex;
    public int CurrentMenuIndex => _currentMenuIndex;

    public MenuRunTime(MenuData info)
    {
        _menuData = info;
        _menuIndex = _menuData.MenuCount;
        _currentMenuIndex = _menuData.DefaultSelectIndex;
    }

    /// <summary>
    /// メニューセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectMenuForKeyboard(int index)
    {
        if (index < 0 || _menuIndex <= index) return;
        _currentMenuIndex = index;
        Debug.Log($"Select : {_currentMenuIndex}");
    }

    /// <summary>
    /// メニューセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectMenuForGamepad(int index)
    {
        if (_currentMenuIndex + index < 0 || _menuIndex <= _currentMenuIndex + index) return;
        _currentMenuIndex += index;
        //行き止まり
        //if (_currentMenuIndex >= _menuData.MenuIndex)
        //{
        //    _currentMenuIndex = _menuData.MenuIndex - 1;
        //}
        //if (_currentMenuIndex <= 0)
        //{
        //    _currentMenuIndex = 0;
        //}

        //ループ
        if (_currentMenuIndex >= _menuData.MenuCount)
        {
            _currentMenuIndex = 0;
        }
        if (_currentMenuIndex < 0)
        {
            _currentMenuIndex = _menuData.MenuCount - 1;
        }
        Debug.Log($"Select : {_currentMenuIndex}");
    }
}
#endregion
