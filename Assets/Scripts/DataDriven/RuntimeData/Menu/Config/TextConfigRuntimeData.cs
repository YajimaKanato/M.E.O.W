using UnityEngine;

namespace DataDriven
{
    /// <summary>テキスト表示に関するランタイムデータ</summary>
    public class TextConfigRuntimeData : MenuCategoryElement
    {
        TextConfigDefaultData _text;
        float[] _textSpeeds;
        int _currentTextSpeedIndex;
        public float CurrentTextSpeed => _textSpeeds[_currentTextSpeedIndex];

        public TextConfigRuntimeData(TextConfigDefaultData text)
        {
            _text = text;
            _currentTextSpeedIndex = (int)_text.DefaultTextSpeed;
            var textSpeeds = _text.TextSpeeds;
            _textSpeeds = new float[textSpeeds.Length];
            for (int i = 0; i < textSpeeds.Length; i++)
            {
                _textSpeeds[i] = textSpeeds[i].Speed;
            }
        }

        public override void ChangeElement(IndexMove move)
        {
            _currentTextSpeedIndex += (int)move;
            if (_currentTextSpeedIndex < 0) _currentTextSpeedIndex = 0;
            if (_currentTextSpeedIndex > _textSpeeds.Length - 1) _currentTextSpeedIndex = _textSpeeds.Length - 1;
        }
    }
}
