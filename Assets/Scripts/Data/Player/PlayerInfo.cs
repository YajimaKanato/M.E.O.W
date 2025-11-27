using Interface;
using UnityEngine;

/// <summary>プレイヤーに関する情報のみを保持するスクリプト</summary>
[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Player/PlayerInfo")]
public class PlayerInfo : InitializeSO, ITalkable
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    [SerializeField] float _hp = 100;
    [SerializeField] float _fullness = 100;
    [SerializeField] float _speed = 20;
    [SerializeField] float _maxWalkSpeed = 5;
    [SerializeField] float _maxRunSpeed = 10;
    [SerializeField] float _jump = 15;
    [SerializeField] ItemInfo[] _items = new ItemInfo[6];
    IItemBaseEffective[] _itemSlot;

    public float HP => _hp;
    public float Fullness => _fullness;
    public float Speed => _speed;
    public float MaxWalkSpeed => _maxWalkSpeed;
    public float MaxRunSpeed => _maxRunSpeed;
    public float Jump => _jump;
    public IItemBaseEffective[] ItemSlot => _itemSlot;

    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;

    public override bool Init(GameManager manager)
    {
        _itemSlot = new IItemBaseEffective[_items.Length];
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i])
            {
                if (_items[i] is IItemBaseEffective)
                {
                    _itemSlot[i] = (IItemBaseEffective)_items[i];
                }
                else
                {
                    FailedInitialization();
                }
            }
        }
        return _isInitialized;
    }
}
