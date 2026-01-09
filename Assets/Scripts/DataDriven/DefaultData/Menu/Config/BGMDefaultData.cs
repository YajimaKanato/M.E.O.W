using UnityEngine;

namespace DataDriven
{
    /// <summary>BGMの初期データ</summary>
    [CreateAssetMenu(fileName = "BGMDefaultData", menuName = "Menu/MenuCategory/Config/BGMDefaultData")]
    public class BGMDefaultData : ConfigCategory
    {
        [SerializeField] float _defaultBGMVolume = 0.7f;
        [SerializeField] float _bgmChangeValue = 0.05f;
        [SerializeField] int _maxBGMVolume = 1;
        [SerializeField] int _minBGMVolume = 0;

        public float DefaultBGMVolume => _defaultBGMVolume;
        public float BGMChangeValue => _bgmChangeValue;
        public (int minBGM, int maxBGM) BGMVolumeRange => (_minBGMVolume, _maxBGMVolume);
    }
}
