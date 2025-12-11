using System.Collections;
using UnityEngine;

public class ItemInstance : InteractBase
{
    [SerializeField] ItemInstanceData _data;

    public override bool Init(GameManager manager)
    {
        base.Init(manager);
        _runtimeDataManager.RegisterData(_id, new ItemInstanceRunTime(_data));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _runtimeDataManager.GetData<ItemInstanceRunTime>(_id).Event();
    }

    /// <summary>
    /// アイテムのデータを設定する関数
    /// </summary>
    /// <param name="item">アイテムの情報</param>
    public void ItemSetting(UsableItem item)
    {
        _runtimeDataManager.GetData<ItemInstanceRunTime>(_id).ItemDataSetting(item);
    }
}
