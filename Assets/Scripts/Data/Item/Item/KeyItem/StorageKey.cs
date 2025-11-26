using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "StorageKey", menuName = "Item/StorageKey")]
public class StorageKey : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
