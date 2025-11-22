using Interface;
using Item;
using UnityEngine;

public abstract class BadFoodBase : ItemInfo, ISaturate, IHealth, IItemBaseEffective
{
    [SerializeField] float _saturate = 10;
    [SerializeField] float _health = 10;

    public float Saturate => _saturate;

    public float Health => _health;

    public void ItemBaseActivate(PlayerInfo player)
    {
        _initManager.GameActionManager.ChangeFullness(this, player);
        _initManager.GameActionManager.ChangeHealth(this, player);
        ItemActivate();
    }

    protected abstract void ItemActivate();
}
