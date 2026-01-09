using UnityEngine;

namespace DataDriven
{
    /// <summary>SEのランタイムデータ</summary>
    public class SERuntimeData : MenuCategoryElement
    {
        SEDefaultData _se;
        float _currentVolume;
        float _changeValue;
        (int min, int max) _volumeRange;
        public float CurrentVolume => _currentVolume;

        public SERuntimeData(SEDefaultData se)
        {
            _se = se;
            _currentVolume = _se.DefaultSEVolume;
            _changeValue = _se.SEChangeValue;
            _volumeRange = _se.SEVolumeRange;
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
