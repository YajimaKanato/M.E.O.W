using Interface;
using Item;
using UnityEngine;

public class Cheese : GoodFoodBase
{
    protected override void ItemTypeSetting()
    {
        _itemType = ItemType.Cheese;
    }
}
