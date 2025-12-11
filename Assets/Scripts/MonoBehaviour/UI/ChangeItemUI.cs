using Interface;
using UnityEngine;

public class ChangeItemUI : UIBehaviour, ISelectableNumberUIForKeyboard, ISelectableNumberUIForGamepad, IClosableUI, IEnterUI
{
    [SerializeField] HotbarData _data;
    [SerializeField] ItemSlot[] _slotImages;
    ChangeItemRunTime _changeItemRunTime;
    int _currentIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        _runtimeDataManager.RegisterData(_id, new ChangeItemRunTime(_data));
        InitializeManager.InitializationForVariable(out _changeItemRunTime, _runtimeDataManager.GetData<ChangeItemRunTime>(_id));
        if (_isInitialized)
        {
            if (_slotImages == null) InitializeManager.FailedInitialization();
        }
        return _isInitialized;
    }

    public void Close()
    {
        _uiManager.NotItemChange();
    }

    public void OpenSetting()
    {
        //アイテムスロットの初期化
        var slot = _changeItemRunTime.ItemSlot;
        var slotLength = slot.Length;
        for (int i = 0; i < slotLength; i++)
        {
            _slotImages[i].ItemSet(slot[i]?.Sprite);
            _slotImages[i].SelectSign(i == _changeItemRunTime.CurrentSlotIndex, _runtimeDataManager.GetData<ChangeItemRunTime>(_id).ChangeItem.Sprite);
        }
    }

    public void PushEnter()
    {
        _uiManager.ItemChange();
    }

    void ISelectableNumberUIForKeyboard.SelectedCategory(int index)
    {
        _changeItemRunTime.SelectItemForKeyboard(index);
        SelectUpdate();
    }

    void ISelectableNumberUIForGamepad.SelectedCategory(int index)
    {
        _changeItemRunTime.SelectItemForGamepad(index);
        SelectUpdate();
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    void SelectUpdate()
    {
        _preSlotIndex = _currentIndex;
        _currentIndex = _changeItemRunTime.CurrentSlotIndex;
        _slotImages[_preSlotIndex].SelectSign(false);
        _slotImages[_currentIndex].SelectSign(true, _runtimeDataManager.GetData<ChangeItemRunTime>(_id).ChangeItem.Sprite);
    }
}
