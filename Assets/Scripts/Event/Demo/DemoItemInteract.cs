using Interface;
using UnityEngine;

public class DemoItemInteract : EventBase, IGiveItemInteract
{
    [SerializeField, Tooltip("プレイヤーが獲得するアイテム")] ItemBase _demoItem;
    public ItemBase Item => _demoItem;

    //この中でイベントを登録
    protected override void EventSetting()
    {
        //順番大事
        _event.Enqueue(A);
        _event.Enqueue(B);
    }

    /// <summary>
    /// アイテムを与えるだけの関数
    /// </summary>
    /// <param name="player"></param>
    void A(PlayerInfo player)
    {
        GameActionManager.GiveItemInteract(this, player.ItemList);
    }

    /// <summary>
    /// ログを流しつつアイテムを与える関数
    /// </summary>
    /// <param name="player"></param>
    void B(PlayerInfo player)
    {
        Debug.Log("Hello!");
        GameActionManager.GiveItemInteract(this, player.ItemList);
    }
}
