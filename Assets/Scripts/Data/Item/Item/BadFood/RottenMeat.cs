using Interface;
using UnityEngine;

public class RottenMeat : BadFoodBase
{
    public override IItemBase ItemBase()
    {
        throw new System.NotImplementedException();
    }

    protected override void ItemActivate()
    {
        Debug.Log("Eat RottenMeat");
    }
}
