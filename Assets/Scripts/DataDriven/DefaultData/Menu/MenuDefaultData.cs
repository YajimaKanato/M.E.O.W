using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューの初期データ</summary>
    [CreateAssetMenu(fileName = "MenuDefaultData", menuName = "Menu/MenuDefaultData")]
    public class MenuDefaultData : ScriptableObject
    {
        [SerializeField] MenuCategory[] _categories;
        [SerializeField] MenuCategory _defaultSelectIndex = MenuCategory.Config;

        public MenuCategory[] Categories => _categories;
        public MenuCategory DefaultSelectIndex => _defaultSelectIndex;
    }
}
