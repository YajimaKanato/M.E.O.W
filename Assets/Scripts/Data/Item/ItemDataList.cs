using UnityEngine;
using System.Collections.Generic;
using Item;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Item/ItemDataList")]
public class ItemDataList : InitializeSO
{
    [SerializeField] List<ItemInfo> _itemList;
    Dictionary<ItemType, ItemInfo> _itemDic;

    public List<ItemInfo> ItemList => _itemList;
    public Dictionary<ItemType, ItemInfo> ItemDic => _itemDic;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();

        _itemDic = new Dictionary<ItemType, ItemInfo>();
        if (_itemDic == null) FailedInitialization();

        foreach (var item in _itemList)
        {
            if (!item) break;
            item.Init(_gameManager);
            _itemDic[item.ItemType] = item;
        }
        return _isInitialized;
    }
}
