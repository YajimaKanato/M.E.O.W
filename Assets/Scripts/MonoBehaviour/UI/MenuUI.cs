using UnityEngine;
using Interface;

public class MenuUI : UIBehaviour, ISelectable, IClosableUI
{
    [SerializeField] MenuSelect[] _menuSelects;
    int _currentSlotIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager)
        {
            FailedInitialization();
        }
        else
        {
            var menuIndex = _gameManager.DataManager.MenuRunTime.MenuIndex;
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

    /// <summary>
    /// メニューの選択中を更新する関数
    /// </summary>
    public void SelectedSlot()
    {
        _preSlotIndex = _currentSlotIndex;
        _currentSlotIndex = _gameManager.DataManager.MenuRunTime.CurrentMenuIndex;
        _menuSelects[_preSlotIndex].gameObject.SetActive(false);
        _menuSelects[_currentSlotIndex].gameObject.SetActive(true);
    }
}
