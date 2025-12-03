using UnityEngine;
using Interface;

public class ItemList : UIBehaviour, ISelectableVerticalArrowUI, ISelectableHorizontalArrowUI
{
    [SerializeField] ItemSlot[] _slot;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        return _isInitialized;
    }

    void SlotUpdate()
    {

    }

    public void GetItem(IItemBase item)
    {

    }


    public void SelectedCategory()
    {

    }
}
