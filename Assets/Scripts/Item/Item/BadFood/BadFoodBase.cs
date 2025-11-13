using Interface;
using UnityEngine;

public abstract class BadFoodBase : ItemBase, ISaturate, IHealth, IItemBaseEffective
{
    protected float _saturate = 10;
    protected float _health = 10;
    public float Saturate => _saturate;

    public float Health => _health;

    public void ItemBaseActivate(PlayerInfo player)
    {
        GameActionManager.Instance.ChangeFullness(this, player.Status);
        GameActionManager.Instance.ChangeHealth(this, player.Status);
    }

    public void ItemUse(ItemList list)
    {
        list.UseItem(this);
    }
}
