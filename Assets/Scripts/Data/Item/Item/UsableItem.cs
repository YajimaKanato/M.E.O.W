using Interface;
using UnityEngine;

/// <summary>プレイヤーが使用できるアイテムのベースクラス</summary>
public class UsableItem : ItemInfo, IItemBaseEffective
{
    protected ObjectManager _dataManager;
    public override bool Init(GameManager manager)
    {
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _dataManager, _gameManager.ObjectManager);
        return _isInitialized;
    }

    public virtual void ItemBaseActivate(int id)
    {
        Debug.LogError("Please Override ItemBaseActivate!");
    }
}
