using Interface;
using UnityEngine;

public abstract class CharacterEventBase : EventBase, IConversationInteract
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;

    public void ConversationInteractStart(PlayerInfo player)
    {
        GameActionManager.ConversationInteract(this, player);
    }
}
