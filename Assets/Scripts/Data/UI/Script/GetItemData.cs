using Interface;
using UnityEngine;

/// <summary>アイテム獲得に関するデータ</summary>
[CreateAssetMenu(fileName = "GetItemData", menuName = "UIData/GetItemData")]
public class GetItemData : InitializeSO
{
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

#region GetItem
/// <summary>アイテム獲得に関するランタイムデータ</summary>
public class GetItemRunTime : IRunTime
{
    GetItemData _getItemData;
    ItemInfo _item;
    Sprite _sprite;
    string _info;
    public ItemInfo Item => _item;
    public Sprite Sprite => _sprite;
    public string Info => _info;

    public GetItemRunTime(GetItemData info)
    {
        _getItemData = info;
    }

    /// <summary>
    /// ゲットしたアイテムの表示設定
    /// </summary>
    /// <param name="item">ゲットしたアイテム</param>
    public void GetItemSetting(ItemInfo item)
    {
        _item = item;
        _sprite = _item.Sprite;
        _info = _item.Info;
    }
}
#endregion
