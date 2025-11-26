using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Camera", menuName = "Item/Camera")]
public class Camera : KeyItemBase
{
    public override IItemBase ItemBase()
    {
        return this;
    }
}
