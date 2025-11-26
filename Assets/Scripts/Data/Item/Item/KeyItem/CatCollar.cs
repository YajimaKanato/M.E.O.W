using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "CatCollar", menuName = "Item/CatCollar")]
public class CatCollar : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
