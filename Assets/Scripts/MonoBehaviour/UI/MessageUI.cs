using Interface;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageUI : UIBehaviour, IEnterUI, IUIOpenAndClose
{
    [SerializeField] Text _text;
    [SerializeField] Image _image;
    [SerializeField] float _textSpeed = 0.1f;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        return _isInitialized;
    }

    /// <summary>
    /// エンター入力時に行う関数
    /// </summary>
    public void PushEnter()
    {
        _gameManager.DataManager.MessageRunTime.PushEnter();
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
        _gameManager.DataManager.MessageRunTime.MessageStart();
        foreach (var t in text)
        {
            //エンター入力が入ったら全文表示
            if (_gameManager.DataManager.MessageRunTime.IsEnter)
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
        _gameManager.DataManager.MessageRunTime.MessageEnd();
    }

    public void OpenSetting()
    {
        _gameManager.DataManager.MessageRunTime.MessageStart();
        _image.sprite = _gameManager.DataManager.MessageRunTime.TextField;
        StartCoroutine(MessageTextCoroutine(_gameManager.DataManager.MessageRunTime.Text));
    }

    public void Close()
    {
        _gameManager.DataManager.MessageRunTime.MessageEnd();
    }
}
