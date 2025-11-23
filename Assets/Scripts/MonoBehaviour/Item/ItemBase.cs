using UnityEngine;

public class ItemBase : InitializeBehaviour
{
    [SerializeField] ItemInfo _itemInfo;
    public ItemInfo ItemInfo => _itemInfo;

    public override void Init(GameManager manager)
    {

    }
}
