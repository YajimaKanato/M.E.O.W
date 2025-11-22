using Interface;
using Item;
using UnityEngine;

public class Alcohol : BadFoodBase
{
    public override IItemBase ItemBase()
    {
        throw new System.NotImplementedException();
    }

    protected override void ItemActivate()
    {
        Debug.Log("Drink Alcohol");
    }
}
