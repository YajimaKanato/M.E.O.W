using UnityEngine;
using Interface;
using Item;

[RequireComponent(typeof(ItemInfo))]
public abstract class KeyItemBase : MonoBehaviour, IItemBase
{
    ItemInfo _info;
    public Sprite Sprite => _info.Sprite;

    public ItemType ItemType => _info.ItemType;

    public ItemRole ItemRole => _info.ItemRole;

    private void Awake()
    {
        _info = GetComponent<ItemInfo>();
    }
}
