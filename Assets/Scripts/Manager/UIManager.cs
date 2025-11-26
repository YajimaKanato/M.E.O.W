using Interface;
using System.Collections;
using UnityEngine;

/// <summary>UIに関する制御を行うクラス</summary>
public class UIManager : InitializeBehaviour
{
    [SerializeField] Sprite _defaultMessageSprite;
    [SerializeField] ConversationUI _conversationUI;
    [SerializeField] MessageUI _messageUI;
    [SerializeField] GetItemUI _getItemUI;
    [SerializeField] Hotbar _hotbar;
    [SerializeField] ItemList _itemList;
    [SerializeField] float _textSpeed = 0.1f;

    ISelectable _currentSelect;

    bool _isEnter = false;
    bool _isTyping = false;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        if (!_conversationUI) return false;
        if (!_conversationUI.Init(manager)) return false;
        _conversationUI.gameObject.SetActive(false);
        if (!_messageUI) return false;
        if (!_messageUI.Init(manager)) return false;
        _messageUI.gameObject.SetActive(false);
        if (!_getItemUI) return false;
        if (!_getItemUI.Init(manager)) return false;
        _getItemUI.gameObject.SetActive(false);
        if (!_hotbar) return false;
        if (!_hotbar.Init(manager)) return false;
        _hotbar.gameObject.SetActive(true);
        //if (!_itemList) return false;
        //if (!_itemList.Init(manager)) return false;
        //_itemList.gameObject.SetActive(false);
        return true;
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
    /// <param name="leftInteract">左側の会話相手の情報を持つインターフェース</param>
    /// <param name="rightInteract">右側の会話相手の情報を持つインターフェース</param>
    public void ConversationSetting(ITalkable leftInteract, ITalkable rightInteract)
    {
        _conversationUI.gameObject.SetActive(true);
        _conversationUI.ConversationSetting(leftInteract, rightInteract);
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
        _messageUI.TextUISetting(_defaultMessageSprite);
        _messageUI.TextUpdate("");
        _isTyping = true;
        var wait = new WaitForSeconds(_textSpeed);
        var s = "";

        foreach (var t in text)
        {
            //エンター入力が入ったら全文表示
            if (_isEnter)
            {
                Debug.Log("Push Enter");
                _messageUI.TextUpdate(text);
                yield return wait;
                break;
            }

            //一文字ずつ追加
            s += t;
            _messageUI.TextUpdate(s);
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

    /// <summary>
    /// アイテムスロットの選択中を切り替える関数
    /// </summary>
    public void SelectedSlot()
    {
        _hotbar.SelectedSlot();
    }

    ///// <summary>
    ///// スロットの選択中を切り替える関数
    ///// </summary>
    //public void SelectedSlot()
    //{
    //    _currentSelect.SelectedSlot();
    //}

    /// <summary>
    /// アイテムに応じてスロットの表示を切り替える関数
    /// </summary>
    /// <param name="item">アイテム</param>
    public void SlotUpdate(IItemBaseEffective item)
    {
        _hotbar.SlotUpdate(item);
    }

    /// <summary>
    /// キーアイテムを獲得した時に呼ばれる関数
    /// </summary>
    /// <param name="item">獲得したアイテム</param>
    public void GetKeyItem(IItemBase item)
    {
        _itemList.GetItem(item);
    }
}
