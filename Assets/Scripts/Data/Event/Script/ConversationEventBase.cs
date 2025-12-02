using Interface;
using UnityEngine;

public abstract class ConversationEventBase : EventBaseData
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;
    
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
}
