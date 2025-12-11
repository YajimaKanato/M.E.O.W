using Interface;
using UnityEngine;

/// <summary>プレイヤーのプレイ中の基本情報を保持するスクリプト</summary>
[CreateAssetMenu(fileName = "PlayerOnPlayScene", menuName = "Player/PlayerOnPlayScene")]
public class PlayerDataOnPlayScene : CharacterDataBase, ITalkable
{
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

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

#region Player
public class PlayerRunTimeOnPlayScene : IRunTime
{
    PlayerDataOnPlayScene _playerInfo;

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

    public PlayerRunTimeOnPlayScene(PlayerDataOnPlayScene info)
    {
        _playerInfo = info;
        _currentHP = info.HP;
        _currentFullness = info.Fullness;
        _speed = info.Speed;
        _maxWalkSpeed = info.MaxWalkSpeed;
        _maxRunSpeed = info.MaxRunSpeed;
        _jump = info.Jump;
    }

    /// <summary>
    /// 満腹度を回復する関数
    /// </summary>
    /// <param name="fullness">回復量</param>
    public void Saturation(float fullness)
    {
        _currentFullness += fullness;
        if (_currentFullness >= _playerInfo.Fullness) _currentFullness = _playerInfo.Fullness;
        Debug.Log($"Saturation => {_currentFullness}");
    }

    /// <summary>
    /// 必要に応じてHPを更新する関数
    /// </summary>
    /// <param name="value">変化量</param>
    public void ChangeHP(float value)
    {
        _currentHP += value;
        if (_currentHP >= _playerInfo.HP) _currentHP = _playerInfo.HP;
        if (_currentHP <= 0) _currentHP = 0;
        Debug.Log($"HP => {_currentHP}");
    }
}
#endregion
