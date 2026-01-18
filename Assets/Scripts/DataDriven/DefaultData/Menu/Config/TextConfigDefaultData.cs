using UnityEngine;

namespace DataDriven
{
    /// <summary>テキスト表示に関する初期データ</summary>
    [CreateAssetMenu(fileName = "TextConfigDefaultData", menuName = "Menu/MenuCategory/Config/TextConfigDefaultData")]
    public class TextConfigDefaultData : ScriptableObject
    {
        [SerializeField] TextSpeedData[] _textSpeeds;
        [SerializeField] TextSpeed _defaultTextSpeedIndex = TextSpeed.Normal;

        public TextSpeedData[] TextSpeeds => _textSpeeds;
        public TextSpeed DefaultTextSpeed => _defaultTextSpeedIndex;
    }

    /// <summary>テキスト表示速度に関するクラス</summary>
    [System.Serializable]
    public class TextSpeedData
    {
        [SerializeField] TextSpeed _speedType;
        [SerializeField] float _speed;

        public TextSpeed SpeedType => _speedType;
        public float Speed => _speed;
    }
}
