using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューの初期データ</summary>
    [CreateAssetMenu(fileName = "MenuDefaultData", menuName = "Menu/MenuDefaultData")]
    public class MenuDefaultData : ScriptableObject
    {
        [SerializeField] MenuType _defaultSelectIndex = MenuType.Config;
        [SerializeField] MenuCategory _category;
        
        public int DefaultSelectIndex => (int)_defaultSelectIndex;
        public MenuCategory Category => _category;
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
