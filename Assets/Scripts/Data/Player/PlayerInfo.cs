using UnityEngine;

/// <summary>プレイヤーに関する情報のみを保持するスクリプト</summary>
[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Player/PlayerInfo")]
public class PlayerInfo : InitializeObject
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    [SerializeField] float _hp;
    [SerializeField] float _fullness;
    [SerializeField] float _speed = 20;
    [SerializeField] float _maxWalkSpeed = 5;
    [SerializeField] float _maxRunSpeed = 10;
    [SerializeField] float _jump = 5;

    public float HP => _hp;
    public float Fullness => _fullness;
    public float Speed => _speed;
    public float MaxWalkSpeed => _maxWalkSpeed;
    public float MaxRunSpeed => _maxRunSpeed;
    public float Jump => _jump;

    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;

    public override void Init(GameManager manager)
    {
        Debug.Log($"{this} has Initialized");
    }
}
