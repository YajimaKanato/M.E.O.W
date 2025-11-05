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
    /// <param name="saturate">ISaturateを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤー</param>
    public static void ChangeFullness(ISaturate saturate, PlayerAction player)
    {
        player.Saturation(saturate.Saturate);
    }

    /// <summary>
    /// アイテムを獲得する関数
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <param name="list">アイテムリスト</param>
    public static void ItemValueUpdate(ItemBase item, ItemList list)
    {

    }
}
