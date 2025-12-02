using Interface;
using System.Linq;
using UnityEngine;

public class MessageRunTime : IRunTime
{
    MessageData _messageData;
    Sprite[] _textFields;
    Sprite _textField;
    public Sprite TextField => _textField;
    string _text;
    public string Text => _text;
    bool _isEnter = false;
    bool _isTyping = false;
    public bool IsEnter => _isEnter;
    public bool IsTyping => _isTyping;

    public MessageRunTime(MessageData info)
    {
        _messageData = info;
        _textFields = info.TextFields;
    }

    /// <summary>
    /// 表示するテキスト等を設定する関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <param name="index">使用するテキストフィールドのインデックス</param>
    public void TextFieldSetting(string text,int index)
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
