using Interface;
using Item;
using UnityEngine;

public class Fish : ISaturate, IItemBaseEffective
{
    protected float _saturate = 10;
    public float Saturate => _saturate;
    protected ItemType _itemType;
    public ItemType ItemType => _itemType;
    protected ItemRole _itemRole;
    public ItemRole ItemRole => _itemRole;

    public void ItemBaseActivate(PlayerInfo player)
    {
        GameActionManager.Instance.ChangeFullness(this, player.Status);
    }

    public void ItemUse(ItemList list)
    {
        //list.UseItem(this);
    }

    public void ItemRoleSetting()
    {
        throw new System.NotImplementedException();
    }

    public void ItemTypeSetting()
    {
        throw new System.NotImplementedException();
    }
}
