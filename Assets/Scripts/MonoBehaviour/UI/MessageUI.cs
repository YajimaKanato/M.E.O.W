using Interface;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>メッセーjのUIに関する制御を行うクラス</summary>
public class MessageUI : UIBehaviour, IEnterUI, IUIOpenAndClose
{
    [SerializeField, Tooltip("メッセージのデータ")] MessageData _data;
    [SerializeField, Tooltip("メッセージを表示するテキスト")] Text _text;
    [SerializeField, Tooltip("テキストフィールドのイメージ")] Image _image;
    MessageRunTime _messageRunTime;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        //ランタイムデータ
        _runtimeDataManager.RegisterData(_id, new MessageRunTime(_data));
        _isInitialized = InitializeManager.InitializationForVariable(out _messageRunTime, _runtimeDataManager.GetData<MessageRunTime>(_id));
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
    /// メッセージを更新する関数
    /// </summary>
    public void MessageUpdate()
    {
        _image.sprite = _messageRunTime.TextField;
        StartCoroutine(MessageTextCoroutine(_messageRunTime.Text));
    }

    /// <summary>
    /// 会話テキストを任意の速度で流す関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <returns></returns>
    IEnumerator MessageTextCoroutine(string text)
    {
        _text.text = "";
        var wait = new WaitForSeconds(_messageRunTime.MessageSpeed);
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

    }

    public void Close()
    {
        _messageRunTime.MessageEnd();
    }
}
