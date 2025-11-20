using Interface;
using Item;
using UnityEngine;

public class Camera : IItemBase
{
    public ItemType ItemType => throw new System.NotImplementedException();

    public ItemRole ItemRole => throw new System.NotImplementedException();

    public void ItemRoleSetting()
    {
        throw new System.NotImplementedException();
    }

    public void ItemTypeSetting()
    {
        throw new System.NotImplementedException();
    }
}
