using Interface;
using UnityEngine;

public class DemoTalkInteract : EventBase, IConversationInteract
{
    public string CharacterName => throw new System.NotImplementedException();

    public Sprite CharacterImage => throw new System.NotImplementedException();

    public void ConversationInteractStart(PlayerInfo player)
    {

    }

    protected override void EventSetting()
    {

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
