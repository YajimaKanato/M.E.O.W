using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューの初期データ</summary>
    [CreateAssetMenu(fileName = "MenuDefaultData", menuName = "Menu/MenuDefaultData")]
    public class MenuDefaultData : ScriptableObject
    {
        [SerializeField] MenuType _defaultSelectIndex = MenuType.Config;
        [SerializeField] MenuCategory[] _categories;

        public MenuType DefaultSelectIndex => _defaultSelectIndex;
        public MenuCategory[] Categories => _categories;
    }

    /// <summary>メニューの状態を表すラベル</summary>
    public enum MenuType
    {
        [InspectorName("設定")] Config,
        [InspectorName("アイテムリスト")] ItemList,
        [InspectorName("会話ログ")] Log,
        [InspectorName("操作説明")] Info
    }
}
