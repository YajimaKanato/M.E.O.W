using Interface;
using UnityEngine;

public abstract class GoodFoodBase : UsableItem, ISaturate
{
    [SerializeField] float _saturate = 10;

    public float Saturate => _saturate;

    public override void ItemBaseActivate(int id)
    {
        _dataManager.ChangeFullness(this, id);
    }
}
