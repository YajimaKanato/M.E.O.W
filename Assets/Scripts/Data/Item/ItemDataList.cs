using UnityEngine;
using System.Collections.Generic;
using Item;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Item/ItemDataList")]
public class ItemDataList : InitializSO
{
    [SerializeField] List<ItemInfo> _itemList;
    Dictionary<ItemType, ItemInfo> _itemDic;

    public List<ItemInfo> ItemList => _itemList;
    public Dictionary<ItemType, ItemInfo> ItemDic => _itemDic;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) return false;

        _itemDic = new Dictionary<ItemType, ItemInfo>();
        if (_itemDic == null) return false;

        foreach (var item in _itemList)
        {
            if (!item) continue;
            _itemDic[item.ItemType] = item;
        }
        return true;
    }
}
