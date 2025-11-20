using Interface;
using Item;
using UnityEngine;

[RequireComponent(typeof(ItemInfo))]
public class GoodFoodBase : MonoBehaviour, ISaturate, IItemBaseEffective
{
    [SerializeField] float _saturate = 10;
    ItemInfo _info;

    public Sprite Sprite => _info.Sprite;

    public ItemType ItemType => _info.ItemType;

    public ItemRole ItemRole => _info.ItemRole;
    public float Saturate => _saturate;

    private void Awake()
    {
        _info = GetComponent<ItemInfo>();
    }

    public void ItemBaseActivate(PlayerInfo player)
    {
        throw new System.NotImplementedException();
    }
}
