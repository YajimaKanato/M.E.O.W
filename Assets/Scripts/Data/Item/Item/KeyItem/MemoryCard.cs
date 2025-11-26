using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "MemoryCard", menuName = "Item/MemoryCard")]
public class MemoryCard : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
