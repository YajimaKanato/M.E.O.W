using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Chocolate", menuName = "Item/Chocolate")]
public class Chocolate : BadFoodBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }

    protected override void ItemActivate()
    {
        Debug.Log("Eat Chocolate");
    }
}
