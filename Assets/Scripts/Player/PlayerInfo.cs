using UnityEngine;

/// <summary>プレイヤーに関する情報のみを保持するスクリプト</summary>
public class PlayerInfo : MonoBehaviour
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    [SerializeField] PlayerCurrentStatus _status;
    [SerializeField] ItemList _itemList;
    [SerializeField] Hotbar _itemSlot;
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
    public PlayerCurrentStatus Status => _status;
    public ItemList ItemList => _itemList;
    public Hotbar ItemSlot => _itemSlot;
}
