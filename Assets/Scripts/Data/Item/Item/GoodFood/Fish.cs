using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Item/Fish")]
public class Fish : GoodFoodBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
