using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーの初期データ</summary>
    [CreateAssetMenu(fileName = "PlayerDefaultData", menuName = "Player/PlayerDefaultData")]
    public class PlayerDefaultData : ScriptableObject
    {
        [Header("CharacterInfo")]
        [SerializeField] Sprite _characterImage;
        [SerializeField] string _characterName;
        [Header("Status")]
        [SerializeField] float _hp = 100;
        [SerializeField] float _fullness = 100;
        [SerializeField] float _speed = 20;
        [SerializeField] float _maxWalkSpeed = 5;
        [SerializeField] float _maxRunSpeed = 10;
        [SerializeField] float _jump = 15;

        public Sprite CharacterImage => _characterImage;
        public string CharacterName => _characterName;
        public float HP => _hp;
        public float Fullness => _fullness;
        public float Speed => _speed;
        public float MaxWalkSpeed => _maxWalkSpeed;
        public float MaxRunSpeed => _maxRunSpeed;
        public float Jump => _jump;
    }
}
