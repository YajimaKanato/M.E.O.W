using Interface;
using UnityEngine;
using UnityEngine.UI;

/// <summary>会話時に表示するUIオブジェクトにアタッチするスクリプト</summary>
public class ConversationUI : UIBehaviour
{
    [Header("LeftCharacter")]
    [SerializeField] Text _leftCharacterNameText;
    [SerializeField] Image _leftCharacterImage;
    [Header("RightCharacter")]
    [SerializeField] Text _rightCharacterNameText;
    [SerializeField] Image _rightCharacterImage;

    /// <summary>
    /// 会話の初めの設定を行う関数
    /// </summary>
    /// <param name="leftInteract">左側の会話相手の情報を持つインターフェース</param>
    /// <param name="rightInteract">右側の会話相手の情報を持つインターフェース</param>
    public void ConversationSetting(ITalkable leftInteract, ITalkable rightInteract)
    {
        _rightCharacterNameText.text = rightInteract.CharacterName;
        _rightCharacterImage.sprite = rightInteract.CharacterImage;

        _leftCharacterNameText.text = leftInteract.CharacterName;
        _leftCharacterImage.sprite = leftInteract.CharacterImage;
    }

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        return _isInitialized;
    }
}
