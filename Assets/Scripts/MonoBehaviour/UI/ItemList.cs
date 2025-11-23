using UnityEngine;
using System.Collections.Generic;
using Interface;

public class ItemList : InitializeBehaviour
{
    [SerializeField] ItemSlot[] _slot;

    void SlotUpdate()
    {

    }

    public void GetItem(IItemBase item)
    {

    }

    public override void Init(GameManager manager)
    {
        Debug.Log($"{this} has Initialized");
    }
}
