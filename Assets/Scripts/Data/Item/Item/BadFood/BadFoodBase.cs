using Interface;
using UnityEngine;

/// <summary>プレイヤーに悪影響を及ぼす食べ物のベースクラス</summary>
public abstract class BadFoodBase : UsableItem, ISaturate, IHealth
{
    [SerializeField, Tooltip("満腹度回復量")] float _saturate = 10;
    [SerializeField, Tooltip("体力回復量")] float _health = 10;

    public float Saturate => _saturate;
    public float Health => _health;

    public override void ItemBaseActivate(int id)
    {
        _dataManager.ChangeFullness(this, id);
        _dataManager.ChangeHealth(this, id);
        ItemActivate();
    }

    protected abstract void ItemActivate();
}
