using Interface;
using UnityEngine;

public class PlayerRunTime
{
    PlayerInfo _playerInfo;
    Sprite _characterImage;

    IItemBaseEffective[] _itemSlot;
    int _currentSlotIndex = 0;
    public int CurrentSlotIndex => _currentSlotIndex;
    int _currentMenuIndex = 0;
    public int CurrentMenuIndex => _currentMenuIndex;
    const int MAXMENU = 4;
    public int MaxMenu => MAXMENU;

    float _currentHP;
    float _currentFullness;
    float _speed;
    float _maxWalkSpeed;
    float _maxRunSpeed;
    float _jump;
    string _characterName;

    public Sprite CharacterImage => _characterImage;
    public string CharacterName => _characterName;
    public float CurrentHP => _currentHP;
    public float CurrentFullness => _currentFullness;
    public float Speed => _speed;
    public float MaxWalkSpeed => _maxWalkSpeed;
    public float MaxRunSpeed => _maxRunSpeed;
    public float Jump => _jump;

    public PlayerRunTime(PlayerInfo info)
    {
        _itemSlot = info.ItemSlot;
        _characterName = info.CharacterName;
        _playerInfo = info;
        _currentHP = info.HP;
        _currentFullness = info.Fullness;
        _speed = info.Speed;
        _maxWalkSpeed = info.MaxWalkSpeed;
        _maxRunSpeed = info.MaxRunSpeed;
        _jump = info.Jump;
        _characterImage = info.CharacterImage;
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

    /// <summary>
    /// アイテムを獲得する関数
    /// </summary>
    /// <param name="item">獲得するアイテム</param>
    /// <returns>獲得できたかどうか</returns>
    public bool GetItem(IItemBase item)
    {
        for (int i = 0; i < _playerInfo.ItemSlot.Length; i++)
        {
            if (_itemSlot[i] == null)
            {
                _itemSlot[i] = (IItemBaseEffective)item;
                return true;
            }
        }

        //アイテムスロットいっぱいの時の処理
        return false;
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForKeyboard(int index)
    {
        _currentSlotIndex = index;
        Debug.Log($"Select : {_currentSlotIndex} => " + (_itemSlot[_currentSlotIndex] != null ? _itemSlot[_currentSlotIndex].ItemType : "null"));
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForGamepad(int index)
    {
        _currentSlotIndex += index;
        //行き止まり
        //if (_currentSlotIndex >= _playerInfo.ItemSlot.Length)
        //{
        //    _currentSlotIndex = _playerInfo.ItemSlot.Length - 1;
        //}
        //if (_currentSlotIndex <= 0)
        //{
        //    _currentSlotIndex = 0;
        //}

        //ループ
        if (_currentSlotIndex >= _playerInfo.ItemSlot.Length)
        {
            _currentSlotIndex = 0;
        }
        if (_currentSlotIndex < 0)
        {
            _currentSlotIndex = _playerInfo.ItemSlot.Length - 1;
        }
        Debug.Log($"Select : {_currentSlotIndex}");
    }

    /// <summary>
    /// アイテムを使用するときに呼ばれる関数
    /// </summary>
    /// <returns>使用したアイテムの情報</returns>
    public IItemBaseEffective UseItem()
    {
        IItemBaseEffective item = _itemSlot[_currentSlotIndex];
        _itemSlot[_currentSlotIndex] = null;
        return item;
    }

    /// <summary>
    /// メニューセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectMenuForKeyboard(int index)
    {
        _currentMenuIndex = index;
        Debug.Log($"Select : {_currentMenuIndex} => " + (_itemSlot[_currentMenuIndex] != null ? _itemSlot[_currentMenuIndex].ItemType : "null"));
    }

    /// <summary>
    /// メニューセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectMenuForGamepad(int index)
    {
        _currentMenuIndex += index;
        //行き止まり
        //if (_currentMenuIndex >= MAXMENU)
        //{
        //    _currentMenuIndex = MAXMENU - 1;
        //}
        //if (_currentMenuIndex <= 0)
        //{
        //    _currentMenuIndex = 0;
        //}

        //ループ
        if (_currentMenuIndex >= MAXMENU)
        {
            _currentMenuIndex = 0;
        }
        if (_currentMenuIndex < 0)
        {
            _currentMenuIndex = MAXMENU - 1;
        }
        Debug.Log($"Select : {_currentMenuIndex}");
    }
}
