using Interface;
using Item;
using UnityEngine;

[RequireComponent(typeof(ItemInfo))]
public abstract class BadFoodBase : MonoBehaviour, ISaturate, IHealth, IItemBaseEffective
{
    [SerializeField] float _saturate = 10;
    [SerializeField] float _health = 10;
    ItemInfo _info;
    GameActionManager _gameActionManager;

    public Sprite Sprite => _info.Sprite;
    public ItemType ItemType => _info.ItemType;
    public ItemRole ItemRole => _info.ItemRole;
    public float Saturate => _saturate;

    public float Health => _health;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _info = GetComponent<ItemInfo>();
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
