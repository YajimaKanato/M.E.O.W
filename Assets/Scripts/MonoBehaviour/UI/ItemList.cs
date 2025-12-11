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

    void ISelectableVerticalArrowUI.SelectedCategory(int index)
    {
        throw new System.NotImplementedException();
    }

    void ISelectableHorizontalArrowUI.SelectedCategory(int index)
    {
        throw new System.NotImplementedException();
    }
}
