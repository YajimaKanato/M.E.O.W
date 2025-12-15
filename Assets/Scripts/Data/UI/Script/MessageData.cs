using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "MessageData", menuName = "UIData/MessageData")]
public class MessageData : InitializeSO
{
    [SerializeField] Sprite[] _textFields;
    [SerializeField] float _messageSpeed = 0.1f;
    public Sprite[] TextFields => _textFields;
    public float MessageSpeed => _messageSpeed;
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

#region Message
public class MessageRunTime : IRunTime
{
    MessageData _messageData;
    Sprite[] _textFields;
    Sprite _textField;
    public Sprite TextField => _textField;
    string _text;
    float _messageSpeed;
    public string Text => _text;
    public float MessageSpeed => _messageSpeed;
    bool _isEnter = false;
    bool _isTyping = false;
    public bool IsEnter => _isEnter;
    public bool IsTyping => _isTyping;

    public MessageRunTime(MessageData info)
    {
        _messageData = info;
        _textFields = info.TextFields;
        _messageSpeed = info.MessageSpeed;
    }

    /// <summary>
    /// 表示するテキスト等を設定する関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <param name="index">使用するテキストフィールドのインデックス</param>
    public void TextFieldSetting(string text, int index)
    {
        _text = text;
        _textField = _textFields[index];
    }

    /// <summary>
    /// 会話が開始した時の処理を行う関数
    /// </summary>
    public void MessageStart()
    {
        _isEnter = false;
        _isTyping = true;
        Debug.Log("Message Start");
    }

    /// <summary>
    /// エンター入力時に行う関数
    /// </summary>
    public void PushEnter()
    {
        //テキスト表示中の処理
        if (_isTyping)
        {
            _isEnter = true;
            Debug.Log("Message Skip");
        }
    }

    /// <summary>
    /// 会話が終わったときの処理を行う関数
    /// </summary>
    public void MessageEnd()
    {
        _isEnter = false;
        _isTyping = false;
        Debug.Log("Message End");
    }
}
#endregion
