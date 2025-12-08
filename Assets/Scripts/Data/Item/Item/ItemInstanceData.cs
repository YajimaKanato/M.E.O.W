using UnityEngine;

[CreateAssetMenu(fileName = "ItemInstanceData", menuName = "Item/ItemInstanceData")]
public class ItemInstanceData : InitializeSO
{
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
