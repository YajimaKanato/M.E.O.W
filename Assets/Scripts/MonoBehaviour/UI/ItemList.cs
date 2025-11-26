using UnityEngine;
using Interface;

public class ItemList : UIBehaviour,ISelectable
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
        return _isInitialized;
    }

    public void SelectedSlot()
    {

    }
}
