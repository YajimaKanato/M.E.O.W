using UnityEngine;

namespace DataDriven
{
    /// <summary>設定のランタイムデータ</summary>
    public class ConfigRuntimeData : MenuCategoryRuntime, IRuntime
    {
        ConfigDefaultData _config;
        ConfigType[] _categories;
        int _currentIndex;
        float _currentBGMVolume;
        float _bgmChangeValue;
        (int max, int min) _bgmVolumeRange;
        float _currentSEVolume;
        float _seChangeValue;
        (int max, int min) _seVolumeRange;

        public ConfigType Category => _categories[_currentIndex];
        public float CurrentBGMVolume => _currentBGMVolume;
        public float CurrentSEVolume => _currentSEVolume;

        public ConfigRuntimeData(ConfigDefaultData config)
        {
            _config = config;
            _categories = config.Categories;
            _currentIndex = (int)config.DefaultCategory;
            _currentBGMVolume = _config.DefaultBGMVolume;
            _bgmChangeValue = _config.BGMChangeValue;
            _bgmVolumeRange = _config.BGMVolumeRange;
            _currentSEVolume = _config.DefaultSEVolume;
            _seChangeValue = _config.SEChangeValue;
            _seVolumeRange = _config.SEVolumeRange;
        }

        public override void SelectCategory(SlotMoveVertical move)
        {
            _currentIndex += (int)move;
            if (_currentIndex < 0) _currentIndex = 0;
            if (_currentIndex > _categories.Length - 1) _currentIndex = _categories.Length - 1;
        }

        /// <summary>
        /// BGMの大きさを変更する関数
        /// </summary>
        /// <param name="dir">変更する方向</param>
        public void BGMVolumeChange(int dir)
        {
            _currentBGMVolume += _bgmChangeValue * dir;
            if (_currentBGMVolume < _bgmVolumeRange.min)
            {
                _currentBGMVolume = _bgmVolumeRange.min;
            }
            if (_currentBGMVolume > _bgmVolumeRange.max)
            {
                _currentBGMVolume = _bgmVolumeRange.max;
            }
        }

        /// <summary>
        /// SEの大きさを変更する関数
        /// </summary>
        /// <param name="dir">変更する方向</param>
        public void SEVolumeChange(int dir)
        {
            _currentSEVolume += _seChangeValue * dir;
            if (_currentSEVolume < _seVolumeRange.min)
            {
                _currentSEVolume = _seVolumeRange.min;
            }
            if (_currentSEVolume > _seVolumeRange.max)
            {
                _currentSEVolume = _seVolumeRange.max;
            }
        }
    }
}
