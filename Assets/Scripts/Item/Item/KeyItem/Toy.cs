using Item;
using UnityEngine;

public class Toy : KeyItemBase
{
    protected override void ItemTypeSetting()
    {
        _itemType = ItemType.Toy;
    }
}
