using UnityEngine;
using Interface;

[System.Serializable]
public class ItemList : InitializeBehaviour
{
    [SerializeField] ItemSlot[] _slot;

    void SlotUpdate()
    {

    }

    public void GetItem(IItemBase item)
    {

    }

    public override bool Init(GameManager manager)
    {
        return true;
    }
}
