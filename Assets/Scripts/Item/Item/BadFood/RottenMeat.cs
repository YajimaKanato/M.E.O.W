using Interface;
using Item;
using UnityEngine;

public class RottenMeat : BadFoodBase
{
    protected override void ItemTypeSetting()
    {
        _itemType = ItemType.RottenMeat;
    }
}
