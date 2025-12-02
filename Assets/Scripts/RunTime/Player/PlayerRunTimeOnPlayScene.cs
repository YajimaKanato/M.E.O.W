using Interface;
using UnityEngine;

public class PlayerRunTimeOnPlayScene : IRunTime, ITalkable
{
    PlayerDataOnPlayScene _playerInfo;

    Sprite _sprite;
    string _name;
    float _currentHP;
    float _currentFullness;
    float _speed;
    float _maxWalkSpeed;
    float _maxRunSpeed;
    float _jump;

    public Sprite CharacterImage => _sprite;
    public string CharacterName => _name;
    public float CurrentHP => _currentHP;
    public float CurrentFullness => _currentFullness;
    public float Speed => _speed;
    public float MaxWalkSpeed => _maxWalkSpeed;
    public float MaxRunSpeed => _maxRunSpeed;
    public float Jump => _jump;

    public PlayerRunTimeOnPlayScene(PlayerDataOnPlayScene info)
    {
        _playerInfo = info;
        _sprite = info.CharacterImage;
        _name = info.CharacterName;
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
