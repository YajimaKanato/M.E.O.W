using Interface;
using UnityEngine;

public class ChangeItemUI : UIBehaviour, ISelectableNumberUIForKeyboard, ISelectableNumberUIForGamepad, IUIOpenAndClose, IEnterUI
{
    [SerializeField] ItemSlot[] _slotImages;
    ChangeItemRunTime _changeItemRunTime;
    HotbarRunTime _hotbarRunTime;
    int _currentIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _changeItemRunTime, _gameManager.DataManager.ChangeItemRunTime);
        InitializeManager.InitializationForVariable(out _hotbarRunTime, _gameManager.DataManager.HotbarRunTime);
        if (_isInitialized)
        {
            if (_slotImages == null) InitializeManager.FailedInitialization();
        }
        return _isInitialized;
    }

    public void Close()
    {

    }

    public void OpenSetting()
    {
        //アイテムスロットの初期化
        var slot = _changeItemRunTime.ItemSlot;
        var slotLength = slot.Length;
        for (int i = 0; i < slotLength; i++)
        {
            if (!_slotImages[i])
            {
                InitializeManager.FailedInitialization();
                break;
            }
            _slotImages[i].ItemSet(slot[i]?.Sprite);
            _slotImages[i].SelectSign(i == 0);
        }
    }

    public void PushEnter()
    {
        
    }

    void ISelectableNumberUIForKeyboard.SelectedCategory(int index)
    {
        _changeItemRunTime.SelectItemForKeyboard(index);
        SelectUpdate(index);
    }

    void ISelectableNumberUIForGamepad.SelectedCategory(int index)
    {
        _changeItemRunTime.SelectItemForGamepad(index);
        SelectUpdate(index);
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    /// <param name="index">切り替えるインデックス</param>
    void SelectUpdate(int index)
    {
        _preSlotIndex = _currentIndex;
        _currentIndex = _changeItemRunTime.CurrentSlotIndex;
        _slotImages[_preSlotIndex].SelectSign(false);
        _slotImages[_currentIndex].SelectSign(true);
    }
}
