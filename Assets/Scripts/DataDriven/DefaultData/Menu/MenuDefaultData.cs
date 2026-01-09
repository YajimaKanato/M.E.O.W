using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューの初期データ</summary>
    [CreateAssetMenu(fileName = "MenuDefaultData", menuName = "Menu/MenuDefaultData")]
    public class MenuDefaultData : ScriptableObject
    {
        [SerializeField] MenuType[] _categories;
        [SerializeField] MenuType _defaultSelectIndex = MenuType.Config;

        public MenuType[] Categories => _categories;
        public MenuType DefaultSelectIndex => _defaultSelectIndex;
    }
}
