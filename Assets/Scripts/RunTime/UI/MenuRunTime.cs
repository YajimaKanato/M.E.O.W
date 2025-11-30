using Interface;
using UnityEngine;

public class MenuRunTime : IRunTime
{
    MenuData _menuData;

    int _menuIndex;
    public int MenuIndex => _menuIndex;
    int _currentMenuIndex = 0;
    public int CurrentMenuIndex => _currentMenuIndex;

    public MenuRunTime(MenuData info)
    {
        _menuData = info;
        _menuIndex = _menuData.MenuCount;
    }

    /// <summary>
    /// メニューセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectMenuForKeyboard(int index)
    {
        _currentMenuIndex = index;
        Debug.Log($"Select : {_currentMenuIndex}");
    }

    /// <summary>
    /// メニューセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectMenuForGamepad(int index)
    {
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
