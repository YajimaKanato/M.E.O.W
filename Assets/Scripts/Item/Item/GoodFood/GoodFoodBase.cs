using Interface;
using UnityEngine;

public abstract class GoodFoodBase : ItemBase, ISaturate, IItemBaseEffective
{
    protected float _saturate = 10;
    public float Saturate => _saturate;

    public void ItemBaseActivate(PlayerInfo player)
    {
        GameActionManager.Instance.ChangeFullness(this, player.Status);
    }

    public void ItemUse(ItemList list)
    {
        list.UseItem(this);
    }
}
