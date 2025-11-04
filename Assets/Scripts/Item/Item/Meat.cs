using Interface;
using UnityEngine;

public class Meat : ItemBase, ISaturate
{
    float _saturate = 10;

    public float Saturate => _saturate;

    public override void ItemActivate(PlayerAction player)
    {
        GameEventManager.FullnessManagement(this, player);
    }
}
