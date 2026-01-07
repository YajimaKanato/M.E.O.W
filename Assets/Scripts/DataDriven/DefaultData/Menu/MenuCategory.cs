using UnityEngine;

namespace DataDriven
{
    /// <summary>メニュー項目の初期ベースデータ</summary>
    public abstract class MenuCategory : ScriptableObject
    {
        [SerializeField] MenuType _menuType = MenuType.Config;

        public MenuType MenuType => _menuType;
    }
}
