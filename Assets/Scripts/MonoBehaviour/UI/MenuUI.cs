using UnityEngine;
using Interface;

public class MenuUI : UIBehaviour, ISelectableNumberUI, IClosableUI, IUIOpenAndClose
{
    [SerializeField] MenuSelect[] _menuSelects;
    MenuRunTime _menuRunTime;
    int _currentSlotIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        InitializationForVariable(out _gameManager, manager);
        InitializationForVariable(out _menuRunTime, _gameManager.DataManager.MenuRunTime);
        if (_isInitialized)
        {
            var menuIndex = _menuRunTime.MenuIndex;
            for (int i = 0; i < menuIndex; i++)
            {
                if (!_menuSelects[i])
                {
                    FailedInitialization();
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

    /// <summary>
    /// メニューの選択中を更新する関数
    /// </summary>
    public void SelectedCategory()
    {
        _preSlotIndex = _currentSlotIndex;
        _currentSlotIndex = _menuRunTime.CurrentMenuIndex;
        _menuSelects[_preSlotIndex].gameObject.SetActive(false);
        _menuSelects[_currentSlotIndex].gameObject.SetActive(true);
    }
}
