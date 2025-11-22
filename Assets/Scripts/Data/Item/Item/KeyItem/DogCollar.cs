using Interface;
using Item;
using UnityEngine;

[CreateAssetMenu(fileName = "DogCollar", menuName = "Item/DogCollar")]
public class DogCollar : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
