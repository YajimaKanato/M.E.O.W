using UnityEngine;

[System.Serializable]
public class ItemInstance : InitializeBehaviour
{
    [SerializeField] ItemInfo _itemInfo;
    public ItemInfo ItemInfo => _itemInfo;

    public override bool Init(GameManager manager)
    {
        return true;
    }
}
