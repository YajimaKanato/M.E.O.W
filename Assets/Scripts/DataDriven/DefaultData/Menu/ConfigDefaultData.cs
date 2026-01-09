using UnityEngine;

namespace DataDriven
{
    /// <summary>設定の初期データ</summary>
    [CreateAssetMenu(fileName = "Config", menuName = "Menu/MenuCategory/Config")]
    public class ConfigDefaultData : MenuCategory
    {
        [SerializeField] ConfigType[] _categories;
        [SerializeField] ConfigType _defaultCategory = ConfigType.BGM;
        [Header("BGM")]
        [SerializeField] float _defaultBGMVolume = 0.7f;
        [SerializeField] float _bgmChangeValue = 0.05f;
        [SerializeField] int _maxBGMVolume = 1;
        [SerializeField] int _minBGMVolume = 0;
        [Header("SE")]
        [SerializeField] float _defaultSEVolume = 0.7f;
        [SerializeField] float _seChangeValue = 0.05f;
        [SerializeField] int _maxSEVolume = 1;
        [SerializeField] int _minSEVolume = 0;

        public ConfigType[] Categories => _categories;
        public ConfigType DefaultCategory => _defaultCategory;
        public float DefaultBGMVolume => _defaultBGMVolume;
        public float BGMChangeValue => _bgmChangeValue;
        public (int minBGM, int maxBGM) BGMVolumeRange => (_minBGMVolume, _maxBGMVolume);
        public float DefaultSEVolume => _defaultSEVolume;
        public float SEChangeValue => _seChangeValue;
        public (int minSE, int maxSE) SEVolumeRange => (_minSEVolume, _maxSEVolume);
    }
}
