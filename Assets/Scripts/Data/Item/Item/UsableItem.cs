using Interface;
using UnityEngine;

/// <summary>プレイヤーが使用できるアイテムのベースクラス</summary>
public abstract class UsableItem : ItemInfo, IItemBaseEffective
{
    protected ObjectManager _dataManager;
    public override bool Init(GameManager manager)
    {
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _dataManager, _gameManager.ObjectManager);
        return _isInitialized;
    }

    public abstract void ItemBaseActivate(int id);
}
