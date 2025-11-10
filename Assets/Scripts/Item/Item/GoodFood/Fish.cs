using Interface;
using Item;
using UnityEngine;

public class Fish : GoodFoodBase
{
    protected override void ItemTypeSetting()
    {
        _itemType = ItemType.Fish;
    }
}
