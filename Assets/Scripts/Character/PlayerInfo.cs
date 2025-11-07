using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    ItemList _itemList;
    ItemSlot _itemSlot;

    /// <summary>
    /// ƒf[ƒ^‚ğİ’è‚·‚éŠÖ”
    /// </summary>
    /// <param name="itemList"></param>
    /// <param name="itemSlot"></param>
    public void DataSetting(ItemList itemList, ItemSlot itemSlot)
    {
        _itemList = itemList;
        _itemSlot = itemSlot;
    }
}
