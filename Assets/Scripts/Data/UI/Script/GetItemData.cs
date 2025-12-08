using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "GetItemData", menuName = "UIData/GetItemData")]
public class GetItemData : UIDataBase
{
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

#region GetItem
public class GetItemRunTime : IRunTime
{
    GetItemData _getItemData;
    Sprite _sprite;
    string _info;
    public Sprite Sprite => _sprite;
    public string Info => _info;

    public GetItemRunTime(GetItemData info)
    {
        _getItemData = info;
    }

    /// <summary>
    /// ゲットしたアイテムの表示設定
    /// </summary>
    /// <param name="sprite">アイテムの画像</param>
    /// <param name="info">アイテムの説明</param>
    public void GetItemSetting(Sprite sprite, string info)
    {
        _sprite = sprite;
        _info = info;
    }
}
#endregion
