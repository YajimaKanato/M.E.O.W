using Interface;
using UnityEngine;

public abstract class GoodFoodBase : ItemInfo, ISaturate, IItemBaseEffective
{
    [SerializeField] float _saturate = 10;
    public float Saturate => _saturate;

    public void ItemBaseActivate(PlayerInfo player)
    {
        _initManager.GameActionManager.ChangeFullness(this, player);
    }
}
