using Interface;
using UnityEngine;

public abstract class ConversationEventBase : EventBase, IConversationInteract
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
}
