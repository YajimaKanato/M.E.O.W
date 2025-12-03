using UnityEngine;
using System.Collections.Generic;
using Item;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "UIData/ItemDataList")]
public class ItemListData : InitializeSO
{
    [SerializeField] List<ItemInfo> _itemList;
    public List<ItemInfo> ItemList => _itemList;

    public override bool Init(GameManager manager)
    {
        if (_itemList == null)
        {
            InitializeManager.FailedInitialization();
        }
        else
        {
            foreach (var item in _itemList)
            {
                if (!item) InitializeManager.FailedInitialization();
                item.Init(manager);
            }
        }
        return _isInitialized;
    }
}
