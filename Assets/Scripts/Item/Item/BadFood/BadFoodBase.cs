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
        GameEventManager.ChangeFullness(this, player.Status);
        GameEventManager.ChangeHealth(this, player.Status);
    }

    public void ItemUse(ItemList list)
    {
        list.UseItem(this);
    }
}
