using UnityEngine;
using System.Collections.Generic;

/// <summary>アイテムのデータリスト</summary>
[CreateAssetMenu(fileName = "ItemDataList", menuName = "Item/ItemDataList")]
public class ItemDataList : InitializeSO
{
    [SerializeField, Tooltip("アイテムのデータリスト")] List<ItemInfo> _itemList;
    public List<ItemInfo> ItemList => _itemList;

    public override bool Init(GameManager manager)
    {
        if (_itemList == null)
        {
            _isInitialized = InitializeManager.FailedInitialization();
        }
        else
        {
            foreach (var item in _itemList)
            {
                if (!item) _isInitialized = InitializeManager.FailedInitialization();
                item?.Init(manager);
            }
        }
        return _isInitialized;
    }
}
