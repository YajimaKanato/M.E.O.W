using UnityEngine;

[CreateAssetMenu(fileName = "HotbarData", menuName = "UIData/HotbarData")]
public class HotbarData : UIDataBase
{
    [SerializeField] UsableItem[] _itemSlot = new UsableItem[6];
    public UsableItem[] ItemSlot => _itemSlot;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
