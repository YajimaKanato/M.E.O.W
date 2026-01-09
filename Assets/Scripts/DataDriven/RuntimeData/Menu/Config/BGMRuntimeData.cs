using UnityEngine;

namespace DataDriven
{
    /// <summary>BGMのランタイムデータ</summary>
    public class BGMRuntimeData : MenuCategoryElement
    {
        BGMDefaultData _bgm;
        float _currentVolume;
        float _changeValue;
        (int min, int max) _volumeRange;
        public float CurrentVolume => _currentVolume;

        public BGMRuntimeData(BGMDefaultData bgm)
        {
            _bgm = bgm;
            _currentVolume = _bgm.DefaultBGMVolume;
            _changeValue = _bgm.BGMChangeValue;
            _volumeRange = _bgm.BGMVolumeRange;
        }

        public override void ChangeElement(IndexMove move)
        {
            _currentVolume += _changeValue * (int)move;
            if (_currentVolume < _volumeRange.min)
            {
                _currentVolume = _volumeRange.min;
            }
            if (_currentVolume > _volumeRange.max)
            {
                _currentVolume = _volumeRange.max;
            }
        }
    }
}
