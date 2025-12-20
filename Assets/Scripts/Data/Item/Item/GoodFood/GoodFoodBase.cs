using Interface;
using UnityEngine;

/// <summary>プレイヤーにいい影響を与える食べ物のベースクラス</summary>
public abstract class GoodFoodBase : UsableItem, ISaturate
{
    [SerializeField, Tooltip("満腹度回復量")] float _saturate = 10;

    public float Saturate => _saturate;

    public override void ItemBaseActivate(int id)
    {
        _dataManager.ChangeFullness(this, id);
    }
}
