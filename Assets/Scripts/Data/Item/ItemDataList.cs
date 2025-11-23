using UnityEngine;
using System.Collections.Generic;
using Item;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Item/ItemDataList")]
public class ItemDataList : InitializeObject
{
    [SerializeField] List<ItemInfo> _itemList;
    Dictionary<ItemType, ItemInfo> _itemDic = new Dictionary<ItemType, ItemInfo>();

    public List<ItemInfo> ItemList => _itemList;
    public Dictionary<ItemType, ItemInfo> ItemDic => _itemDic;

    public override void Init(GameManager manager)
    {
        _gameManager = manager;
        foreach (var item in _itemList)
        {
            if (!item) continue;
            _itemDic[item.ItemType] = item;
        }
        Debug.Log($"{this} has Initialized");
    }
}
