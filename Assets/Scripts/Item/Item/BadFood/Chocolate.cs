using Item;
using UnityEngine;

public class Chocolate : BadFoodBase
{
    protected override void ItemTypeSetting()
    {
        _itemType = ItemType.Chocolate;
    }
}
