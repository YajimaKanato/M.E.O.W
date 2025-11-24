using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] ItemInfo _itemInfo;
    public ItemInfo ItemInfo => _itemInfo;
}
