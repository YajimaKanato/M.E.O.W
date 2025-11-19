using UnityEngine;
using System.Collections.Generic;
using Interface;
using UnityEngine.InputSystem;

public class ItemSlot : MonoBehaviour
{
    PlayerInputActionManager _playerInputActionManager;

    IItemBaseEffective[] _itemSlot;

    int _slotIndex;
    const int MAXSLOT = 6;
    private void Awake()
    {

    }

    void Init()
    {
        _playerInputActionManager = PlayerInputActionManager.Instance;
        _itemSlot = new IItemBaseEffective[MAXSLOT];
    }

    /// <summary>
    /// アイテムを獲得する関数
    /// </summary>
    /// <param name="item">獲得するアイテム</param>
    public void GetItem(ItemBase item)
    {

    }

    public void SlotSet()
    {

    }

    /// <summary>
    /// アイテムスロットの情報を更新する関数
    /// </summary>
    public void SlotUpdate()
    {

    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    /// <returns>選んだアイテム</returns>
    public IItemBaseEffective SelectItemForKeyboard(int index)
    {
        _slotIndex = index;
        Debug.Log($"Select : {_slotIndex}");
        return _itemSlot[index];
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    /// <returns>選んだアイテム</returns>
    public IItemBaseEffective SelectItemForGamepad(int index)
    {
        _slotIndex += index;
        if (_slotIndex >= MAXSLOT)
        {
            _slotIndex = MAXSLOT;
        }
        if (_slotIndex <= 0)
        {
            _slotIndex = 0;
        }
        Debug.Log($"Select : {_slotIndex}");
        return _itemSlot[index];
    }
}
