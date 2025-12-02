using UnityEngine;

public class ItemInstance : InitializeBehaviour
{
    [SerializeField] ItemInfo _itemInfo;
    public ItemInfo ItemInfo => _itemInfo;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
