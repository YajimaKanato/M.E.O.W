using UnityEngine;
using Interface;

public class MenuUI : UIBehaviour, ISelectableNumberUIForKeyboard, ISelectableNumberUIForGamepad, ISelectableHorizontalArrowUI, ISelectableVerticalArrowUI, IClosableUI, IUIOpenAndClose
{
    [SerializeField] MenuData _menu;
    [SerializeField] ItemListData _itemList;
    [SerializeField] MenuSelect[] _menuSelects;
    MenuRunTime _menuRunTime;
    int _currentIndex = 0;
    int _preIndex = 0;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        _runtimeDataManager.RegisterData(_id, new MenuRunTime(_menu));
        InitializeManager.InitializationForVariable(out _menuRunTime, _runtimeDataManager.GetData<MenuRunTime>(_id));
        return _isInitialized;
    }

    public void Close()
    {

    }

    public void OpenSetting()
    {
        var menuIndex = _menuRunTime.MenuIndex;
        for (int i = 0; i < menuIndex; i++)
        {
            _menuSelects[i].gameObject.SetActive(i == _currentIndex);
        }
    }

    void ISelectableNumberUIForKeyboard.SelectedCategory(int index)
    {
        _menuRunTime.SelectMenuForKeyboard(index);
        SelectUpdate();
    }

    void ISelectableNumberUIForGamepad.SelectedCategory(int index)
    {
        _menuRunTime.SelectMenuForGamepad(index);
        SelectUpdate();
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    void SelectUpdate()
    {
        _preIndex = _currentIndex;
        _currentIndex = _menuRunTime.CurrentMenuIndex;
        _menuSelects[_preIndex].gameObject.SetActive(false);
        _menuSelects[_currentIndex].gameObject.SetActive(true);
    }

    void ISelectableHorizontalArrowUI.SelectedCategory(int index)
    {
        if (_menuSelects[_currentIndex] is ISelectableHorizontalArrowUI) ((ISelectableHorizontalArrowUI)_menuSelects[_currentIndex]).SelectedCategory(index);
    }

    void ISelectableVerticalArrowUI.SelectedCategory(int index)
    {
        if (_menuSelects[_currentIndex] is ISelectableVerticalArrowUI) ((ISelectableVerticalArrowUI)_menuSelects[_currentIndex]).SelectedCategory(index);
    }
}
