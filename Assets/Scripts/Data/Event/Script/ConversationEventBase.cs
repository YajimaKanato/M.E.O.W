using UnityEngine;

public abstract class ConversationEventBase : EventBaseData
{
    [SerializeField] string _characterName;
    [SerializeField] Sprite _characterImage;

    protected DataManager _dataManager;
    protected ConversationRunTime _conversationRunTime;
    protected PlayerRunTimeOnPlayScene _playerRunTimeOnPlayScene;

    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
}
