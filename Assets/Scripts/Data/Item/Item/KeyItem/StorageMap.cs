using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "StorageMap", menuName = "Item/StorageMap")]
public class StorageMap : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
