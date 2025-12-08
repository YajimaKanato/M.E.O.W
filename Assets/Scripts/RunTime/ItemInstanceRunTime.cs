using Interface;
using UnityEngine;

public class ItemInstanceRunTime : IRunTime
{
    ItemInstanceData _itemInstanceData;
    UsableItem _item;
    public UsableItem Item => _item;
    public ItemInstanceRunTime(ItemInstanceData info)
    {
        _itemInstanceData = info;
    }

    /// <summary>
    /// 表示するアイテムの情報を設定する関数
    /// </summary>
    /// <param name="item">アイテムの情報</param>
    public void ItemDataSetting(UsableItem item)
    {
        _item = item;
    }
}
