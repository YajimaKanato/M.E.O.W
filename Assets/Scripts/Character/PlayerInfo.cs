using UnityEngine;

/// <summary>プレイヤーに関する情報のみを保持するスクリプト</summary>
public class PlayerInfo : MonoBehaviour
{
    [SerializeField] PlayerCurrentStatus _status;
    [SerializeField] ItemList _itemList;
    [SerializeField] ItemSlot _itemSlot;

    public PlayerCurrentStatus Status => _status;
    public ItemList ItemList => _itemList;
    public ItemSlot ItemSlot => _itemSlot;
}
