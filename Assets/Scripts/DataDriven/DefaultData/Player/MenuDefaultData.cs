using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューの初期データ</summary>
    [CreateAssetMenu(fileName = "MenuDefaultData", menuName = "Player/MenuDefaultData")]
    public class MenuDefaultData : ScriptableObject
    {
        [SerializeField] MenuType _defaultSelectIndex = MenuType.Config;
        [SerializeField] float _bgmVolume;
        [SerializeField] float _seVolume;

        public int DefaultSelectIndex => (int)_defaultSelectIndex;
        public float BGMVolume => _bgmVolume;
        public float SEVolume => _seVolume;
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
