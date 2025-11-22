using UnityEngine;
using System.Collections.Generic;
using Interface;

public class ItemList : MonoBehaviour,IInitialize
{
    [SerializeField] ItemSlot[] _slot;

    private void Start()
    {
        
    }

    void SlotUpdate()
    {

    }

    public void GetItem(IItemBase item)
    {

    }

    public void Init(GameManager manager)
    {
        Debug.Log($"{this} has Initialized");
    }
}
