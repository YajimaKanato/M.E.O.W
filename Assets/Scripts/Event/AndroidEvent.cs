using UnityEngine;
using System;
using Interface;

public class AndroidEvent : EventBase, IConversationInteract
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;

    public void ConversationInteractStart(PlayerInfo player)
    {

    }

    protected override void EventSetting()
    {

    }
}
