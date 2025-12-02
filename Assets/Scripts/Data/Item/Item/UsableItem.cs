using Interface;
using UnityEngine;

public class UsableItem : ItemInfo, IItemBaseEffective
{
    public virtual void ItemBaseActivate()
    {
        Debug.LogError("Please Override ItemBaseActivate!");
    }
}
