using Interface;
using UnityEngine;
using UnityEngine.UI;

/// <summary>会話時に表示するUI全体のオブジェクトにアタッチするスクリプト</summary>
public class ConversationUI : MonoBehaviour
{
    [Header("LeftCharacter")]
    [SerializeField] Text _leftCharacterNameText;
    [SerializeField] Image _leftCharacterImage;
    [Header("RightCharacter")]
    [SerializeField] Text _rightCharacterNameText;
    [SerializeField] Image _rightCharacterImage;

    /// <summary>
    /// 会話の初めに設定を行う関数
    /// </summary>
    /// <param name="interact">会話を行うクラス</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ConversationSetting(IConversationInteract interact, PlayerInfo player)
    {
        _rightCharacterNameText.text = interact.CharacterName;
        _rightCharacterImage.sprite = interact.CharacterImage;

        _leftCharacterNameText.text = player.CharacterName;
        _leftCharacterImage.sprite = player.CharacterImage;
    }
}
