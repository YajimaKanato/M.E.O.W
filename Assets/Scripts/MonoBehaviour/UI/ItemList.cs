using UnityEngine;
using Interface;

public class ItemList : UIBehaviour, ISelectableUI
{
    [SerializeField] ItemSlot[] _slot;
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        return _isInitialized;
    }

    void SlotUpdate()
    {

    }

    public void GetItem(IItemBase item)
    {

    }


    public void SelectedSlot()
    {

    }
}
