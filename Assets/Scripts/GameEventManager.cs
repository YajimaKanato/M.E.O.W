using Interface;
using UnityEngine;

/// <summary>ゲーム内のイベントに関する制御を行うスクリプト</summary>
public class GameEventManager// : MonoBehaviour
{
    //static GameEventManager _instance;
    //private void Awake()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    /// <summary>
    /// プレイヤーの体力を管理する関数
    /// </summary>
    /// <param name="health">IHealthを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤー</param>
    public static void ChangeHealth(IHealth health, PlayerAction player)
    {
        player.ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">rISatuateを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤー</param>
    public static void ChangeFullness(ISaturate saturate, PlayerAction player)
    {
        player.Saturation(saturate.Saturate);
    }

    /// <summary>
    /// アイテムを獲得させる関数
    /// </summary>
    /// <param name="interact">IGiveItemInteractを実装したスクリプトのインスタンス</param>
    /// <param name="list">アイテムリスト</param>
    public static void GiveItemInteract(IGiveItemInteract interact, ItemList list)
    {
        list.GetItem(interact.Item);
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <param name="list">アイテムリスト</param>
    public static void ItemUse(ItemBase item, ItemList list)
    {
        list.UseItem(item);
    }


    public static void TalkInteract(ITalkInteract interact)
    {

    }

}
