using Interface;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageUI : UIBehaviour, IEnterUI
{
    [SerializeField] Text _text;
    [SerializeField] Image _image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float _textSpeed = 0.1f;

    bool _isEnter = false;
    bool _isTyping = false;
    public bool IsTyping => _isTyping;
    public float TextSpeed => _textSpeed;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    /// <summary>
    /// テキストフィールドを設定する関数
    /// </summary>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void TextUISetting(int index)
    {
        _image.sprite = sprites[index];
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
        }
    }

    /// <summary>
    /// テキストを更新する関数
    /// </summary>
    /// <param name="text">更新する文字列</param>
    public void TextUpdate(string text)
    {
        StartCoroutine(MessageTextCoroutine(text));
    }

    /// <summary>
    /// 会話テキストを任意の速度で流す関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <returns></returns>
    IEnumerator MessageTextCoroutine(string text)
    {
        _text.text = "";
        _isTyping = true;
        var wait = new WaitForSeconds(_textSpeed);
        var s = "";

        foreach (var t in text)
        {
            //エンター入力が入ったら全文表示
            if (_isEnter)
            {
                Debug.Log("Push Enter");
                _text.text = text;
                yield return wait;
                break;
            }

            //一文字ずつ追加
            s += t;
            _text.text = s;
            yield return wait;
        }
        yield return wait;

        _isEnter = false;
        _isTyping = false;
    }
}
