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
    /// <param name="player">プレイヤーの情報</param>
    public static void ChangeHealth(IHealth health, PlayerCurrentStatus player)
    {
        player.ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISatuateを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤーの情報</param>
    public static void ChangeFullness(ISaturate saturate, PlayerCurrentStatus player)
    {
        player.Saturation(saturate.Saturate);
    }

    /// <summary>
    /// インタラクトを行う関数
    /// </summary>
    /// <typeparam name="T">任意のインターフェースを継承している型</typeparam>
    /// <param name="interact">任意のインターフェースを実装したスクリプトのインスタンス</param>
    /// <param name="itemList">アイテムリスト</param>
    public static void Interact<T>(T interact, ItemList itemList)
    {
        //if (interact is ITalkInteract) interact.TalkInteract();
        //if (interact is IGiveItemOnlyInteract) itemList.GetItem(interact.Item);
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <param name="list">アイテムリスト</param>
    public static void ItemUse(ItemBase item, PlayerInfo player)
    {
        player.ItemList.UseItem(item);
        item.ItemActivate(player.Status);
    }

}
