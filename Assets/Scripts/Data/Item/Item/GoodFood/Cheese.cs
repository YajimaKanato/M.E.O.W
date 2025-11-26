using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Cheese", menuName = "Item/Cheese")]
public class Cheese : GoodFoodBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
