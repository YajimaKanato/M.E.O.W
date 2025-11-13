using Interface;
using System.Collections;
using UnityEngine;

/// <summary>ゲーム内のイベントに関する制御を行うスクリプト</summary>
public class GameActionManager// : MonoBehaviour
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

    static IEnumerator _eventEnumerator;

    #region アイテム関連
    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <param name="player">プレイヤーの情報</param>
    public static void ItemUse(IItemBaseEffective item, PlayerInfo player)
    {
        item.ItemBaseActivate(player);
        item.ItemUse(player.ItemList);
    }

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
    #endregion

    #region インタラクト関連
    /// <summary>
    /// インタラクトを行う関数
    /// </summary>
    /// <param name="interact">インタラクトを行うクラス</param>
    /// <param name="player">プレイヤーの情報</param>
    /// <returns>イベントの流れ</returns>
    public static void Interact(EventBase interact, PlayerInfo player)
    {
        if (_eventEnumerator == null)
        {
            _eventEnumerator = interact.Event(player);
        }
        else
        {
            Debug.Log("Already Event Happened");
        }
    }

    /// <summary>
    /// アイテムを与えるインタラクトを行う関数
    /// </summary>
    /// <param name="interact">インタラクトを行うクラス</param>
    /// <param name="itemList">アイテムリスト</param>
    public static void GiveItemInteract(IGiveItemInteract interact, ItemList itemList)
    {
        itemList.GetItem(interact.Item);
    }
    #endregion

    /// <summary>
    /// エンター入力に対するアクションを行う関数
    /// </summary>
    public static void PushEnterUntilTalking()
    {
        if (StoryManager.PushEnter())
        {
            //テキスト表示中

        }
        else
        {
            //テキスト表示終了
            if (_eventEnumerator != null)
            {
                //次のテキストなどを表示
                if (!_eventEnumerator.MoveNext())
                {
                    _eventEnumerator = null;
                }
            }
        }
    }
}
