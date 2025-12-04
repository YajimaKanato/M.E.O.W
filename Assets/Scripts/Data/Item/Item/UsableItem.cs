using Interface;
using UnityEngine;

public class UsableItem : ItemInfo, IItemBaseEffective
{
    protected DataManager _dataManager;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _dataManager, _gameManager.DataManager);
        return _isInitialized;
    }

    public virtual void ItemBaseActivate()
    {
        Debug.LogError("Please Override ItemBaseActivate!");
    }
}
