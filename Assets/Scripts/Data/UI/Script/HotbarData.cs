using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "HotbarData", menuName = "UIData/HotbarData")]
public class HotbarData : InitializeSO
{
    [SerializeField] UsableItem[] _itemSlot = new UsableItem[6];
    [SerializeField] int _defaultHotbarSelectIndex = 0;
    [SerializeField] int _defaultChangeSelectIndex = 0;
    public UsableItem[] ItemSlot => _itemSlot;
    public int DefaultHotbarSelectIndex => _defaultHotbarSelectIndex;
    public int DefaultChangeSelectIndex => _defaultChangeSelectIndex;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

#region Hotbar
public class HotbarRunTime : IRunTime
{
    HotbarData _hotbarData;
    UsableItem[] _itemSlot;
    public UsableItem[] ItemSlot => _itemSlot;
    int _currentSlotIndex = 0;
    public int CurrentSlotIndex => _currentSlotIndex;

    public HotbarRunTime(HotbarData info)
    {
        _hotbarData = info;
        _currentSlotIndex = _hotbarData.DefaultHotbarSelectIndex;
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
        if (index < 0 || _itemSlot.Length <= index) return;
        _currentSlotIndex = index;
        Debug.Log($"Select : {_currentSlotIndex} => " + (_itemSlot[_currentSlotIndex] != null ? _itemSlot[_currentSlotIndex].ItemType : "null"));
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForGamepad(int index)
    {
        if (_currentSlotIndex + index < 0 || _itemSlot.Length <= _currentSlotIndex + index) return;
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

    /// <summary>
    /// アイテムを使用するときに呼ばれる関数
    /// </summary>
    /// <returns>使用したアイテムの情報</returns>
    public UsableItem UseItem()
    {
        UsableItem item = _itemSlot[_currentSlotIndex];
        _itemSlot[_currentSlotIndex] = null;
        return item;
    }

    /// <summary>
    /// アイテムを獲得する関数
    /// </summary>
    /// <param name="item">獲得するアイテム</param>
    /// <returns>格納したリストの番号</returns>
    public int GetItem(UsableItem item)
    {
        for (int i = 0; i < _itemSlot.Length; i++)
        {
            if (_itemSlot[i] == null)
            {
                _itemSlot[i] = item;
                return i;
            }
        }

        //アイテムスロットいっぱいの時
        return -1;
    }

    /// <summary>
    /// アイテムを交換する関数
    /// </summary>
    /// <param name="changeItem">交換するアイテム</param>
    /// <param name="index">アイテム交換をするスロットのインデックス</param>
    /// <returns>交換したアイテム</returns>
    public UsableItem ChangeItem(UsableItem changeItem, int index)
    {
        var item = _itemSlot[index];
        _itemSlot[index] = changeItem;
        Debug.Log($"{item} => {changeItem}");
        return item;
    }
}
#endregion

#region ChangeItem
public class ChangeItemRunTime : IRunTime
{
    HotbarData _hotbarData;
    UsableItem _changeItem;
    UsableItem[] _itemSlot;
    public UsableItem ChangeItem => _changeItem;
    public UsableItem[] ItemSlot => _itemSlot;
    int _currentSlotIndex = 0;
    public int CurrentSlotIndex => _currentSlotIndex;

    public ChangeItemRunTime(HotbarData info)
    {
        _hotbarData = info;
        _currentSlotIndex = _hotbarData.DefaultChangeSelectIndex;
        var slot = _hotbarData.ItemSlot;
        var length = slot.Length;
        _itemSlot = new UsableItem[length];
        for (int i = 0; i < length; i++)
        {
            _itemSlot[i] = slot[i];
        }
    }

    /// <summary>
    /// アイテム交換画面を開くときに呼ばれる関数
    /// </summary>
    /// <param name="itemSlot">現在のアイテムスロット</param>
    /// <param name="changeItem">交換するアイテム</param>
    public void ItemChangeSetting(UsableItem[] itemSlot, UsableItem changeItem)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            _itemSlot[i] = itemSlot[i];
        }
        _changeItem = changeItem;
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForKeyboard(int index)
    {
        if (index < 0 || _itemSlot.Length <= index) return;
        _currentSlotIndex = index;
        Debug.Log($"Select : {_currentSlotIndex} => " + (_itemSlot[_currentSlotIndex] != null ? _itemSlot[_currentSlotIndex].ItemType : "null"));
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForGamepad(int index)
    {
        if (_currentSlotIndex + index < 0 || _itemSlot.Length <= _currentSlotIndex + index) return;
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
#endregion
