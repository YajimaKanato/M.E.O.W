using System.Collections.Generic;
using UnityEngine;

public class ItemListRunTime
{
    ItemListData _itemListData;
    Dictionary<ItemInfo, (bool, int)> _itemGetDic;
    public Dictionary<ItemInfo, (bool, int)> ItemGetDic => _itemGetDic;

    public ItemListRunTime(ItemListData itemListData)
    {
        _itemListData = itemListData;
        _itemGetDic = new Dictionary<ItemInfo, (bool, int)>();
        if (_itemListData.ItemList != null)
        {
            foreach (var item in _itemListData.ItemList)
            {
                //初期状態では取得していない、所持数0で登録
                _itemGetDic.Add(item, (false, 0));
            }
        }
    }


}
