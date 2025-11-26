using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Toy", menuName = "Item/Toy")]
public class Toy : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
