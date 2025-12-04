using UnityEngine;

public class ChangeItemRunTime
{
    HotbarData _hotbarData;
    UsableItem[] _itemSlot;
    public UsableItem[] ItemSlot => _itemSlot;
    int _currentSlotIndex = 0;
    public int CurrentSlotIndex => _currentSlotIndex;

    public ChangeItemRunTime(HotbarData info)
    {
        _hotbarData = info;
        var slot = _hotbarData.ItemSlot;
        var length = slot.Length;
        _itemSlot = new UsableItem[length];
        for (int i = 0; i < length; i++)
        {
            _itemSlot[i] = slot[i];
        }
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
        //if (_currentSlotIndex >= _itemSlot.Length)
        //{
        //    _currentSlotIndex = _itemSlot.Length - 1;
        //}
        //if (_currentSlotIndex <= 0)
        //{
        //    _currentSlotIndex = 0;
        //}

        //ループ
        if (_currentSlotIndex >= _itemSlot.Length)
        {
            _currentSlotIndex = 0;
        }
        if (_currentSlotIndex < 0)
        {
            _currentSlotIndex = _itemSlot.Length - 1;
        }
        Debug.Log($"Select : {_currentSlotIndex}");
    }
}
