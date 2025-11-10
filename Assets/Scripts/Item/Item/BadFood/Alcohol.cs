using Interface;
using Item;
using UnityEngine;

public class Alcohol : BadFoodBase
{
    protected override void ItemTypeSetting()
    {
        _itemType = ItemType.Alcohol;
    }
}
