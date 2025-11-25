using Interface;
using UnityEngine;

public class Chocolate : BadFoodBase
{
    public override IItemBase ItemBase()
    {
        throw new System.NotImplementedException();
    }

    protected override void ItemActivate()
    {
        Debug.Log("Eat Chocolate");
    }
}
