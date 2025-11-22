using UnityEngine;

/// <summary>プレイヤーに関する情報のみを保持するスクリプト</summary>
[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Player/PlayerInfo")]
public class PlayerInfo : InitializeObject
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    [SerializeField] PlayerDefaultStatus _status;
    GameManager _initManager;

    float _currentHP;
    float _currentFullness;
    float _speed;
    float _maxWalkSpeed;
    float _maxRunSpeed;
    float _jump;

    public float CurrentHP => _currentHP;
    public float CurrentFullness => _currentFullness;
    public float Speed => _speed;
    public float MaxWalkSpeed => _maxWalkSpeed;
    public float MaxRunSpeed => _maxRunSpeed;
    public float Jump => _jump;

    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
    public GameManager InitManager => _initManager;

    public override void Init(GameManager manager)
    {
        _initManager = manager;
        _currentHP = _status.HP;
        _currentFullness = _status.Fullness;
        _speed = _status.Speed;
        _maxWalkSpeed = _status.MaxWalkSpeed;
        _maxRunSpeed = _status.MaxRunSpeed;
        _jump = _status.Jump;
        Debug.Log($"{this} has Initialized");
    }

    /// <summary>
    /// 満腹度を回復する関数
    /// </summary>
    /// <param name="fullness">回復量</param>
    public void Saturation(float fullness)
    {
        _currentFullness += fullness;
        if (_currentFullness >= _status.Fullness) _currentFullness = _status.Fullness;
        Debug.Log($"Saturation => {_currentFullness}");
    }

    /// <summary>
    /// 必要に応じてHPを更新する関数
    /// </summary>
    /// <param name="value">変化量</param>
    public void ChangeHP(float value)
    {
        _currentHP += value;
        if (_currentHP >= _status.HP) _currentHP = _status.HP;
        if (_currentHP <= 0) _currentHP = 0;
        Debug.Log($"HP => {_currentHP}");
    }
}
