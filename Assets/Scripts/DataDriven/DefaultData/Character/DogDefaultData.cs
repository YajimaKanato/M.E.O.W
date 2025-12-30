using UnityEngine;

namespace DataDriven
{
    /// <summary>キャラクターのベースデータ</summary>
    public class CharacterDefaultData : ScriptableObject
    {
        [Header("CharacterInfo")]
        [SerializeField] Sprite _characterImage;
        [SerializeField] string _characterName;
        [Header("EventData")]
        [SerializeField] EventData _eventData;

        public Sprite CharacterImage => _characterImage;
        public string CharacterName => _characterName;
        public EventData EventData => _eventData;
    }

    /// <summary>犬の初期データ</summary>
    [CreateAssetMenu(fileName = "DogDefaultData", menuName = "Character/DogDefaultData")]
    public class DogDefaultData : CharacterDefaultData
    {

    }
}
