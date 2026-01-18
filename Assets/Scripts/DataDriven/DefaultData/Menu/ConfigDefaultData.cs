using UnityEngine;

namespace DataDriven
{
    /// <summary>設定の初期データ</summary>
    [CreateAssetMenu(fileName = "Config", menuName = "Menu/MenuCategory/Config")]
    public class ConfigDefaultData : ScriptableObject
    {
        [SerializeField] ConfigType[] _categories;
        [SerializeField] ConfigType _defaultCategory = ConfigType.BGM;

        public ConfigType[] Categories => _categories;
        public ConfigType DefaultCategory => _defaultCategory;
    }
}
