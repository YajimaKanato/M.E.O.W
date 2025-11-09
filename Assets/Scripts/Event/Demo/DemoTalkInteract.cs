using Interface;
using UnityEngine;

public class DemoTalkInteract : EventBase, ITalkInteract
{
    protected override void EventSetting()
    {
        //順番大事
        _event.Enqueue(A);
        _event.Enqueue(B);
    }

    /// <summary>
    /// 挨拶する関数
    /// </summary>
    void A(PlayerInfo player)
    {
        Debug.Log("Hello!");
    }

    /// <summary>
    /// バイバイする関数
    /// </summary>
    void B(PlayerInfo player)
    {
        Debug.Log("Bye");
    }
}
