using UnityEngine;
using System.Collections.Generic;
using Interface;

public class ItemList : MonoBehaviour
{
    Dictionary<IItemBase, int> _itemPossessCount = new Dictionary<IItemBase, int>();
    public Dictionary<IItemBase, int> ItemPossessCount => _itemPossessCount;

    /// <summary>
    /// アイテムを獲得する関数
    /// </summary>
    /// <param name="item">獲得するアイテム</param>
    public void GetItem(IItemBase item)
    {
        if (!_itemPossessCount.ContainsKey(item)) _itemPossessCount[item] = 0;
        _itemPossessCount[item]++;
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    /// <param name="item">使用するアイテム</param>
    public void UseItem(IItemBase item)
    {
        if (_itemPossessCount.ContainsKey(item)) _itemPossessCount[item]--;
    }
}
