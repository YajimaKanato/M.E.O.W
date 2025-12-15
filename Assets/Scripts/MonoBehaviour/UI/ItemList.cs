using UnityEngine;
using Interface;

/// <summary>アイテムリストに関する制御を行うクラス</summary>
public class ItemList : MenuBase, ISelectableVerticalArrowUI, ISelectableHorizontalArrowUI
{
    [SerializeField, Tooltip("すべてのアイテムのデータ")] ItemListData _data;
    [SerializeField, Tooltip("アイテムリストのスロット")] ItemListSlot[] _slot;
    [SerializeField, Tooltip("アイテムの情報を表示するUI")] ItemInfoUI _itemInfoUI;
    ItemListRuntime _itemListRuntime;
    int _currentIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);

        //ランタイムデータ関連
        _runtimeDataManager.RegisterData(_id, new ItemListRuntime(_data));
        _isInitialized = InitializeManager.InitializationForVariable(out _itemListRuntime, _runtimeDataManager.GetData<ItemListRuntime>(_id));

        //アサイン関連
        if (_slot == null)
        {
            _isInitialized = InitializeManager.FailedInitialization();
        }
        if (!_itemInfoUI)
        {
            _isInitialized = InitializeManager.FailedInitialization();
        }

        //アイテムスロットの初期化
        var slot = _itemListRuntime.Items;
        var slotLength = slot.Length;
        for (int i = 0; i < slotLength; i++)
        {
            _slot[i].SelectSign(i == _itemListRuntime.CurrentSlotIndex);
            if (slot[i]) _slot[i].ItemSet(_itemListRuntime.GotItemInfo(slot[i]));
        }

        return _isInitialized;
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
        _itemInfoUI.InfoSetting(_itemListRuntime.Item);
    }
}
