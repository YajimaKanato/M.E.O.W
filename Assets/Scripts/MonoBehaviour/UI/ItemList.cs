using UnityEngine;
using Interface;

public class ItemList : MenuSelect, ISelectableVerticalArrowUI, ISelectableHorizontalArrowUI
{
    [SerializeField] ItemListData _data;
    [SerializeField] ItemListSlot[] _slot;
    //[SerializeField] ItemInfoUI _itemInfoUI;
    [SerializeField] int _horizontalIndex = 5;
    [SerializeField] int _verticalIndex = 4;
    ItemListRuntime _itemListRuntime;
    int _currentIndex = 0;
    int _preSlotIndex = 0;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        _runtimeDataManager.RegisterData(_id, new ItemListRuntime(_data));
        InitializeManager.InitializationForVariable(out _itemListRuntime, _runtimeDataManager.GetData<ItemListRuntime>(_id));
        if (_slot == null)
        {
            _isInitialized = InitializeManager.FailedInitialization();
        }
        //if (!_itemInfoUI)
        //{
        //    _isInitialized = InitializeManager.FailedInitialization();
        //}
        return _isInitialized;
    }

    public override void OpenSetting()
    {
        SlotUpdate();
    }

    public override void Close()
    {
        
    }

    void SlotUpdate()
    {
        //アイテムスロットの初期化
        var slot = _itemListRuntime.Items;
        var slotLength = slot.Length;
        for (int i = 0; i < slotLength; i++)
        {
            _slot[i].ItemSet(slot[i]?.Sprite);
            _slot[i].SelectSign(i == _itemListRuntime.CurrentSlotIndex);
            _slot[i].ItemSet(_itemListRuntime.ItemList[slot[i]]);
        }
    }

    void ISelectableVerticalArrowUI.SelectedCategory(int index)
    {
        _itemListRuntime.VerticalSelectItem(index);
        SelectUpdate();
    }

    void ISelectableHorizontalArrowUI.SelectedCategory(int index)
    {
        _itemListRuntime.HorizontalSelectItem(index);
        SelectUpdate();
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    void SelectUpdate()
    {
        _preSlotIndex = _currentIndex;
        _currentIndex = _itemListRuntime.CurrentSlotIndex;
        _slot[_preSlotIndex].SelectSign(false);
        _slot[_currentIndex].SelectSign(true);
    }
}
