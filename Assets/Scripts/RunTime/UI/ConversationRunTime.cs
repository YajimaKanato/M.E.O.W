using Interface;
using UnityEngine;

public class ConversationRunTime : IRunTime
{
    ITalkable _leftTalkCharacter;
    ITalkable _rightTalkCharacter;
    public ITalkable LeftTalkCharacter => _leftTalkCharacter;
    public ITalkable RightTalkCharacter => _rightTalkCharacter;

    public ConversationRunTime(ConversationData info) { }

    /// <summary>
    /// 会話するキャラクターのデータを登録する関数
    /// </summary>
    /// <param name="left">左に登場するキャラクターのデータ</param>
    /// <param name="right">右に登場するキャラクターのデータ</param>
    public void CharacterDataSetting(ITalkable left, ITalkable right)
    {
        _leftTalkCharacter = left;
        _rightTalkCharacter = right;
    }
}
