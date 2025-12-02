using Interface;
using UnityEngine;

public abstract class GoodFoodBase : UsableItem, ISaturate
{
    [SerializeField] float _saturate = 10;

    public float Saturate => _saturate;

    public override void ItemBaseActivate()
    {
        _gameManager.GameActionManager.ChangeFullness(this);
    }
}
