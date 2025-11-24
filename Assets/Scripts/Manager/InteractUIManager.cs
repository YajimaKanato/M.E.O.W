using Interface;
using System.Collections;
using UnityEngine;

public class InteractUIManager : InitializeBehaviour
{
    [SerializeField] ConversationUI _conversationUI;
    [SerializeField] MessageUI _messageUI;
    [SerializeField] GetItemUI _getItemUI;
    [SerializeField] Hotbar _hotbar;
    [SerializeField] ItemList _itemList;
    [SerializeField] float _textSpeed = 0.1f;

    bool _isEnter = false;
    bool _isTyping = false;

    private void Awake()
    {
        //Init();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override void Init(GameManager manager)
    {
        _conversationUI.gameObject.SetActive(false);
        _messageUI.gameObject.SetActive(false);
        _getItemUI.gameObject.SetActive(false);
        Debug.Log($"{this} has Initialized");
    }

    /// <summary>
    /// メッセージボックスを表示する関数
    /// </summary>
    public void MessageOpen()
    {
        _messageUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// メッセージボックスを閉じる関数
    /// </summary>
    public void MessageClose()
    {
        _messageUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// 会話を開始した時に呼びだす関数
    /// </summary>
    /// <param name="interact">会話を行うクラス</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ConversationStart(IConversationInteract interact)
    {
        _conversationUI.gameObject.SetActive(true);
        _conversationUI.ConversationSetting(interact);
    }

    /// <summary>
    /// 会話が終了した時に呼びだす関数
    /// </summary>
    public void ConversationEnd()
    {
        _conversationUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// エンター入力時に行う関数
    /// </summary>
    /// <returns>テキスト表示中かどうか</returns>
    public bool PushEnter()
    {
        //テキスト表示中の処理
        if (_isTyping)
        {
            _isEnter = true;
        }

        return _isTyping;
    }

    /// <summary>
    /// テキストを更新する関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    public void MessageTextUpdate(string text)
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
        _messageUI.TextUpdate("", null);
        _isTyping = true;
        var wait = new WaitForSeconds(_textSpeed);
        var s = "";

        foreach (var t in text)
        {
            //エンター入力が入ったら全文表示
            if (_isEnter)
            {
                Debug.Log("Push Enter");
                _messageUI.TextUpdate(text, null);
                yield return wait;
                break;
            }

            //一文字ずつ追加
            s += t;
            _messageUI.TextUpdate(s, null);
            yield return wait;
        }
        yield return wait;

        _isEnter = false;
        _isTyping = false;
    }

    /// <summary>
    /// アイテム獲得時のUIを表示する関数
    /// </summary>
    /// <param name="item">アイテムの情報</param>
    public void GetItemUIOpen(IGiveItemInteract item)
    {
        _getItemUI.gameObject.SetActive(true);
        _getItemUI.GetItemUIUpdate(item.Item.Info, item.Item.Sprite);
    }

    /// <summary>
    /// アイテム獲得時のUIを閉じる関数
    /// </summary>
    public void GetItemUIClose()
    {
        _getItemUI.gameObject.SetActive(false);
    }
}
