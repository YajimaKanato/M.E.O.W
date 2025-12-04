using Interface;
using UnityEngine;

public abstract class BadFoodBase : UsableItem, ISaturate, IHealth
{
    [SerializeField] float _saturate = 10;
    [SerializeField] float _health = 10;

    public float Saturate => _saturate;
    public float Health => _health;

    public override void ItemBaseActivate()
    {
        _dataManager.ChangeFullness(this);
        _dataManager.ChangeHealth(this);
        ItemActivate();
    }

    protected abstract void ItemActivate();
}
