using Interface;

public abstract class KeyItemBase : ItemInfo
{
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        return _isInitialized;
    }
}
