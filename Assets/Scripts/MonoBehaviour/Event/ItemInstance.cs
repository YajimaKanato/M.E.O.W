using System.Collections;
using UnityEngine;

/// <summary>アイテムの実体に関する制御を行うクラス</summary>
public class ItemInstance : InteractBase
{
    [SerializeField,Tooltip("アイテムの実体のデータ")] ItemInstanceData _data;

    public override bool Init(GameManager manager)
    {
        base.Init(manager);
        //ランタイムデータ
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
