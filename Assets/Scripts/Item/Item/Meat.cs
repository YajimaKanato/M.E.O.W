using Interface;
using UnityEngine;

public class Meat : ItemBase, ISaturate
{
    float _saturate = 10;

    public float Saturate => _saturate;

    public override void ItemActivate(PlayerCurrentStatus status)
    {
        GameEventManager.ChangeFullness(this, status);
    }
}
