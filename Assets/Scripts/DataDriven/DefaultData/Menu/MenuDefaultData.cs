using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューの初期データ</summary>
    [CreateAssetMenu(fileName = "MenuDefaultData", menuName = "Menu/MenuDefaultData")]
    public class MenuDefaultData : ScriptableObject
    {
        [SerializeField] MenuType _defaultSelectIndex = MenuType.Config;
        [SerializeField] ConfigDefaultData _config;
        [SerializeField] ItemCollectionDefaultData _itemCollection;
        [SerializeField] LogDefaultData _log;
        [SerializeField] InfoDefaultData _info;

        public int DefaultSelectIndex => (int)_defaultSelectIndex;
        public ConfigDefaultData Config => _config;
        public ItemCollectionDefaultData ItemCollection => _itemCollection;
        public LogDefaultData Log => _log;
        public InfoDefaultData Info => _info;
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
