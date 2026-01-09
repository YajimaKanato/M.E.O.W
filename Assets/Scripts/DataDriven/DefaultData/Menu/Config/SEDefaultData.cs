using UnityEngine;

namespace DataDriven
{
    /// <summary>SEの初期データ</summary>
    [CreateAssetMenu(fileName = "SEDefaultData", menuName = "Menu/MenuCategory/Config/SEDefaultData")]
    public class SEDefaultData : ConfigCategory
    {
        [SerializeField] float _defaultSEVolume = 0.7f;
        [SerializeField] float _seChangeValue = 0.05f;
        [SerializeField] int _maxSEVolume = 1;
        [SerializeField] int _minSEVolume = 0;

        public float DefaultSEVolume => _defaultSEVolume;
        public float SEChangeValue => _seChangeValue;
        public (int minSE, int maxSE) SEVolumeRange => (_minSEVolume, _maxSEVolume);
    }
}
