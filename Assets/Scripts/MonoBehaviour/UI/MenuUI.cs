using UnityEngine;
using Interface;

public class MenuUI : UIBehaviour, ISelectableNumberUIForKeyboard, ISelectableNumberUIForGamepad, IClosableUI, IUIOpenAndClose
{
    [SerializeField] MenuSelect[] _menuSelects;
    MenuRunTime _menuRunTime;
    int _currentIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _menuRunTime, _gameManager.DataManager.MenuRunTime);
        if (_isInitialized)
        {
            var menuIndex = _menuRunTime.MenuIndex;
            for (int i = 0; i < menuIndex; i++)
            {
                if (!_menuSelects[i])
                {
                    InitializeManager.FailedInitialization();
                    break;
                }
                _menuSelects[i].gameObject.SetActive(i == 0);
            }
        }

        return _isInitialized;
    }

    public void Close()
    {

    }

    public void OpenSetting()
    {

    }

    void ISelectableNumberUIForKeyboard.SelectedCategory(int index)
    {
        _menuRunTime.SelectMenuForKeyboard(index);
        SelectUpdate(index);
    }

    void ISelectableNumberUIForGamepad.SelectedCategory(int index)
    {
        _menuRunTime.SelectMenuForGamepad(index);
        SelectUpdate(index);
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    /// <param name="index">切り替えるインデックス</param>
    void SelectUpdate(int index)
    {
        _preSlotIndex = _currentIndex;
        _currentIndex = _menuRunTime.CurrentMenuIndex;
        _menuSelects[_preSlotIndex].gameObject.SetActive(false);
        _menuSelects[_currentIndex].gameObject.SetActive(true);
    }
}
