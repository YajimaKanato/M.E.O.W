using Interface;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageUI : UIBehaviour, IEnterUI, IUIOpenAndClose
{
    [SerializeField] Text _text;
    [SerializeField] Image _image;
    [SerializeField] float _textSpeed = 0.1f;
    MessageRunTime _messageRunTime;

    public override bool Init(GameManager manager)
    {
        InitializationForVariable(out _gameManager, manager);
        InitializationForVariable(out _messageRunTime, _gameManager.DataManager.MessageRunTime);
        return _isInitialized;
    }

    /// <summary>
    /// エンター入力時に行う関数
    /// </summary>
    public void PushEnter()
    {
        _messageRunTime.PushEnter();
    }

    /// <summary>
    /// 会話テキストを任意の速度で流す関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <returns></returns>
    IEnumerator MessageTextCoroutine(string text)
    {
        _text.text = "";
        var wait = new WaitForSeconds(_textSpeed);
        var s = "";
        _messageRunTime.MessageStart();
        foreach (var t in text)
        {
            //エンター入力が入ったら全文表示
            if (_messageRunTime.IsEnter)
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
        _messageRunTime.MessageEnd();
    }

    public void OpenSetting()
    {
        _messageRunTime.MessageStart();
        _image.sprite = _messageRunTime.TextField;
        StartCoroutine(MessageTextCoroutine(_messageRunTime.Text));
    }

    public void Close()
    {
        _messageRunTime.MessageEnd();
    }
}
