using Interface;
using Item;
using UnityEngine;

public class Fish : GoodFoodBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
