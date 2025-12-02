using Interface;
using UnityEngine;
using UnityEngine.UI;

/// <summary>会話時に表示するUIオブジェクトにアタッチするスクリプト</summary>
public class ConversationUI : UIBehaviour, IUIBase, IUIOpenAndClose
{
    [Header("LeftCharacter")]
    [SerializeField] Text _leftCharacterNameText;
    [SerializeField] Image _leftCharacterImage;
    [Header("RightCharacter")]
    [SerializeField] Text _rightCharacterNameText;
    [SerializeField] Image _rightCharacterImage;
    ConversationRunTime _conversationRunTime;

    public override bool Init(GameManager manager)
    {
        InitializationForVariable(out _gameManager, manager);
        InitializationForVariable(out _conversationRunTime, _gameManager.DataManager.ConversationRunTime);
        return _isInitialized;
    }

    public void Close()
    {

    }

    public void OpenSetting()
    {
        var left = _conversationRunTime.LeftTalkCharacter;
        _leftCharacterNameText.text = left.CharacterName;
        _leftCharacterImage.sprite = left.CharacterImage;

        var right = _conversationRunTime.RightTalkCharacter;
        _rightCharacterNameText.text = right.CharacterName;
        _rightCharacterImage.sprite = right.CharacterImage;
    }
}
