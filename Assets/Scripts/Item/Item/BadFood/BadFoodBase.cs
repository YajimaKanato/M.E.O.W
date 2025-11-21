using Interface;
using Item;
using UnityEngine;

public abstract class BadFoodBase : ItemInfo, ISaturate, IHealth, IItemBaseEffective
{
    [SerializeField] float _saturate = 10;
    [SerializeField] float _health = 10;
    GameActionManager _gameActionManager;

    public float Saturate => _saturate;

    public float Health => _health;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _gameActionManager = GameActionManager.Instance;
    }

    public void ItemBaseActivate(PlayerInfo player)
    {
        _gameActionManager.ChangeFullness(this, player.Status);
        _gameActionManager.ChangeHealth(this, player.Status);
        ItemActivate();
    }

    protected abstract void ItemActivate();
}
