using Interface;

/// <summary>キーアイテムのベースクラス</summary>
public abstract class KeyItemBase : ItemInfo
{
    public override bool Init(GameManager manager)
    {
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        return _isInitialized;
    }
}
