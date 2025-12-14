using Interface;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemListData", menuName = "UIData/ItemListData")]
public class ItemListData : InitializeSO
{
    [SerializeField] ItemInfo[] _items;
    [SerializeField] int _horizontalIndex = 5;
    [SerializeField] int _verticalIndex = 4;
    public ItemInfo[] Items => _items;
    public int HorizontalIndex => _horizontalIndex;
    public int VerticalIndex => _verticalIndex;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

public class ItemListRuntime : IRunTime
{
    ItemListData _itemListData;
    Dictionary<ItemInfo, bool> _itemList;
    public Dictionary<ItemInfo, bool> ItemList => _itemList;
    ItemInfo[] _items;
    public ItemInfo[] Items => _items;
    ItemInfo _item;
    public ItemInfo Item => _item;
    int _currentSlotIndex = 0;
    public int CurrentSlotIndex => _currentSlotIndex;

    public ItemListRuntime(ItemListData info)
    {
        _itemListData = info;
        _items = info.Items;
        _itemList = new Dictionary<ItemInfo, bool>();
        foreach (var item in _items)
        {
            _itemList[item] = false;
        }
    }

    /// <summary>
    /// アイテムを獲得した時に呼ばれる関数
    /// </summary>
    /// <param name="item">獲得したアイテム</param>
    public void GetItem(ItemInfo item)
    {
        _itemList[item] = true;
    }

    /// <summary>
    /// アイテムを使用した時に呼ばれる関数
    /// </summary>
    /// <param name="item">使用したアイテム</param>
    public void UseItem(ItemInfo item)
    {
        _itemList[item] = false;
    }

    /// <summary>
    /// 横方向のアイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void HorizontalSelectItem(int index)
    {
        if (_currentSlotIndex + index < 0 || _items.Length <= _currentSlotIndex + index) return;
        var horizonl = _itemListData.HorizontalIndex;
        if (index == -1 && (_currentSlotIndex + index) % horizonl == horizonl - 1) return;
        if (index == 1 && (_currentSlotIndex + index) % horizonl == 0) return;
        _currentSlotIndex += index;
    }

    /// <summary>
    /// 縦方向のアイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void VerticalSelectItem(int index)
    {
        var vertical = _itemListData.VerticalIndex * index;
        if (_currentSlotIndex + vertical < 0 || _items.Length <= _currentSlotIndex + vertical) return;
        _currentSlotIndex += vertical;
    }
}
