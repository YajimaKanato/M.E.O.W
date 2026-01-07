using UnityEngine;

namespace DataDriven
{
    /// <summary>設定の初期データ</summary>
    [CreateAssetMenu(fileName = "Config", menuName = "Menu/MenuCategory/Config")]
    public class ConfigDefaultData : MenuCategory
    {
        [SerializeField] float _bgmVolume;
        [SerializeField] float _seVolume;

        public float BGMVolume => _bgmVolume;
        public float SEVolume => _seVolume;
    }
}
