using UnityEngine;

[CreateAssetMenu(fileName = "MenuData", menuName = "UIData/MenuData")]
public class MenuData : UIDataBase
{
    [SerializeField] int _menuCount = 4;
    public int MenuCount => _menuCount;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
