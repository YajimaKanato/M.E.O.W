using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーのランタイムデータ</summary>
    public class PlayerRuntimeData : IRuntime
    {
        PlayerDefaultData _player;

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

        public PlayerRuntimeData(PlayerDefaultData player)
        {
            _player = player;

            _currentHP = _player.HP;
            _currentFullness = _player.Fullness;
            _speed = _player.Speed;
            _maxWalkSpeed = _player.MaxWalkSpeed;
            _maxRunSpeed = _player.MaxRunSpeed;
            _jump = _player.Jump;
        }

        /// <summary>
        /// 満腹度を回復する関数
        /// </summary>
        /// <param name="fullness">回復量</param>
        public void Saturation(float fullness)
        {
            _currentFullness += fullness;
            if (_currentFullness >= _player.Fullness) _currentFullness = _player.Fullness;
            if (_currentFullness <= 0) _currentFullness = 0;
        }

        /// <summary>
        /// 必要に応じてHPを更新する関数
        /// </summary>
        /// <param name="value">変化量</param>
        public void ChangeHP(float value)
        {
            _currentHP += value;
            if (_currentHP >= _player.HP) _currentHP = _player.HP;
            if (_currentHP <= 0) _currentHP = 0;
        }
    }
}
