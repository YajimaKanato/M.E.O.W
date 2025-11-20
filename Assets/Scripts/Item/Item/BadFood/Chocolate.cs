using Interface;
using Item;
using UnityEngine;

public class Chocolate : ISaturate, IHealth, IItemBaseEffective
{
    public float Saturate => throw new System.NotImplementedException();

    public ItemType ItemType => throw new System.NotImplementedException();

    public ItemRole ItemRole => throw new System.NotImplementedException();

    public float Health => throw new System.NotImplementedException();

    public Sprite Sprite => throw new System.NotImplementedException();

    public void ItemBaseActivate(PlayerInfo player)
    {
        throw new System.NotImplementedException();
    }

    public void ItemRoleSetting()
    {
        throw new System.NotImplementedException();
    }

    public void ItemTypeSetting()
    {
        throw new System.NotImplementedException();
    }

    public void ItemUse(ItemList list)
    {
        throw new System.NotImplementedException();
    }
}
