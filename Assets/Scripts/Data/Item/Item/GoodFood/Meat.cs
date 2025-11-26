using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Meat", menuName = "Item/Meat")]
public class Meat : GoodFoodBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
