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

    public void Close()
    {

    }

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        return _isInitialized;
    }

    public void OpenSetting()
    {
        var left = _gameManager.DataManager.ConversationRunTime.LeftTalkCharacter;
        _leftCharacterNameText.text = left.CharacterName;
        _leftCharacterImage.sprite = left.CharacterImage;

        var right = _gameManager.DataManager.ConversationRunTime.RightTalkCharacter;
        _rightCharacterNameText.text = right.CharacterName;
        _rightCharacterImage.sprite = right.CharacterImage;
    }
}
