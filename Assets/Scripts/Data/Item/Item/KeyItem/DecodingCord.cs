using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "DecodingCord", menuName = "Item/DecodingCord")]
public class DecodingCord : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
