using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Alcohol", menuName = "Item/Alcohol")]
public class Alcohol : BadFoodBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }

    protected override void ItemActivate()
    {
        Debug.Log("Drink Alcohol");
    }
}
