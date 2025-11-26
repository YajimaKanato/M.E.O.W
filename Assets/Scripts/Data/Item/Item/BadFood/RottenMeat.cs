using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "RottenMeat", menuName = "Item/RottenMeat")]
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
