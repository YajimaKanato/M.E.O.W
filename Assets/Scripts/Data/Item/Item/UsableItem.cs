using Interface;
using UnityEngine;

public class UsableItem : ItemInfo, IItemBaseEffective
{
    protected ObjectManager _dataManager;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _dataManager, _gameManager.ObjectManager);
        return _isInitialized;
    }

    public virtual void ItemBaseActivate(int id)
    {
        Debug.LogError("Please Override ItemBaseActivate!");
    }
}
