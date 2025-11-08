using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    protected virtual void Init()
    {

    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    /// <param name="status">プレイヤーの情報</param>
    public abstract void ItemActivate(PlayerCurrentStatus status);
}
