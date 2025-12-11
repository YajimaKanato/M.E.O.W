using UnityEngine;
using Interface;

public class MenuUI : UIBehaviour, ISelectableNumberUIForKeyboard, ISelectableNumberUIForGamepad, IClosableUI, IUIOpenAndClose
{
    [SerializeField] MenuData _data;
    [SerializeField] MenuSelect[] _menuSelects;
    MenuRunTime _menuRunTime;
    int _currentIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        _runtimeDataManager.RegisterData(_id, new MenuRunTime(_data));
        InitializeManager.InitializationForVariable(out _menuRunTime, _runtimeDataManager.GetData<MenuRunTime>(_id));
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
        _preSlotIndex = _currentIndex;
        _currentIndex = _menuRunTime.CurrentMenuIndex;
        _menuSelects[_preSlotIndex].gameObject.SetActive(false);
        _menuSelects[_currentIndex].gameObject.SetActive(true);
    }
}
