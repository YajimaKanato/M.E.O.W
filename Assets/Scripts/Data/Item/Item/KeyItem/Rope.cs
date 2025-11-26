using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Rope", menuName = "Item/Rope")]
public class Rope : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
