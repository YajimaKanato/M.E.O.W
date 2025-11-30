using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>UIに関する制御を行うクラス</summary>
public class UIManager : InitializeBehaviour
{
    [System.Serializable]
    class UISettings
    {
        [SerializeField] UIBehaviour _ui;
        [SerializeField] bool _isActive = true;

        public UIBehaviour UI => _ui;
        public bool IsActive => _isActive;
    }

    [SerializeField] UISettings[] _uiSettings;
    ConversationUI _conversationUI;
    MessageUI _messageUI;
    GetItemUI _getItemUI;
    Hotbar _hotbar;
    ItemList _itemList;
    MenuUI _menuUI;

    Stack<ISelectable> _selectStack;
    Stack<IClosableUI> _closeStack;
    Stack<IEnterUI> _enterStack;

    bool _isEnter = false;
    bool _isTyping = false;
    bool _isNext = false;
    public bool IsNext => _isNext;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        _selectStack = new Stack<ISelectable>();
        _closeStack = new Stack<IClosableUI>();
        _enterStack = new Stack<IEnterUI>();
        _selectStack.Push(null);
        _closeStack.Push(null);
        _enterStack.Push(null);

        foreach (var ui in _uiSettings)
        {
            if (ui.UI is ConversationUI)
            {
                _conversationUI = ui.UI as ConversationUI;
                if (!_conversationUI) FailedInitialization();
            }
            else if (ui.UI is MessageUI)
            {
                _messageUI = ui.UI as MessageUI;
                if (!_messageUI) FailedInitialization();
            }
            else if (ui.UI is GetItemUI)
            {
                _getItemUI = ui.UI as GetItemUI;
                if (!_getItemUI) FailedInitialization();
            }
            else if (ui.UI is Hotbar)
            {
                _hotbar = ui.UI as Hotbar;
                if (!_hotbar) FailedInitialization();
                if (ui.IsActive) NextSelectableUI(_hotbar);
            }
            else if (ui.UI is ItemList)
            {
                _itemList = ui.UI as ItemList;
                if (!_itemList) FailedInitialization();
            }
            else if (ui.UI is MenuUI)
            {
                _menuUI = ui.UI as MenuUI;
                if (ui.IsActive) NextSelectableUI(_menuUI);
                if (!_menuUI) FailedInitialization();
            }
            ui.UI?.Init(manager);
            ui.UI?.gameObject.SetActive(ui.IsActive);
        }
        return _isInitialized;
    }

    /// <summary>
    /// メッセージボックスを表示する関数
    /// </summary>
    public void MessageOpen()
    {
        _messageUI.gameObject.SetActive(true);
        NextSelectableUI(null);
        NextClosableUI(null);
    }

    /// <summary>
    /// メッセージボックスを閉じる関数
    /// </summary>
    public void MessageClose()
    {
        _messageUI.gameObject.SetActive(false);
        ReturnSelectableUI();
        ReturnClosableUI();
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
    public void PushEnter()
    {
        if (_closeStack.Peek() == null)
        {
            //テキスト表示中の処理
            if (_isTyping)
            {
                _isEnter = true;
            }
        }
        else
        {

        }
    }

    /// <summary>
    /// テキストに関する表示を更新する関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void MessageTextUpdate(string text, int index)
    {
        _messageUI.TextUISetting(index);
        StartCoroutine(MessageTextCoroutine(text));
    }

    /// <summary>
    /// 会話テキストを任意の速度で流す関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <returns></returns>
    IEnumerator MessageTextCoroutine(string text)
    {
        _messageUI.TextUpdate("");
        _isTyping = true;
        var wait = new WaitForSeconds(_messageUI.TextSpeed);
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
    public void GetItemUIOpen(ItemInfo item)
    {
        _getItemUI.gameObject.SetActive(true);
        _getItemUI.GetItemUIUpdate(item?.Info, item?.Sprite);
    }

    /// <summary>
    /// アイテム獲得時のUIを閉じる関数
    /// </summary>
    public void GetItemUIClose()
    {
        _getItemUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// アイテム交換画面を開く関数
    /// </summary>
    public void ItemChangeOpen()
    {
        NextSelectableUI(null);
    }

    /// <summary>
    /// アイテム交換画面を閉じる関数
    /// </summary>
    public void ItemChangeClose()
    {
        ReturnSelectableUI();
    }


    /// <summary>
    /// アイテムに応じてスロットの表示を切り替える関数
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <param name="index">更新するスロット</param>
    public void SlotUpdate(UsableItem item, int index = -1)
    {
        _hotbar.SlotUpdate(item?.Sprite, index);
    }

    /// <summary>
    /// キーアイテムを獲得した時に呼ばれる関数
    /// </summary>
    /// <param name="item">獲得したアイテム</param>
    public void GetKeyItem(IItemBase item)
    {
        _itemList.GetItem(item);
    }

    /// <summary>
    /// メニューを開く関数
    /// </summary>
    /// <returns>メニューを開いたかどうか</returns>
    public bool OpenMenu()
    {
        if (!(_closeStack.Peek() is MenuUI))
        {
            NextSelectableUI(_menuUI);
            NextClosableUI(_menuUI);
            _menuUI.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    /// <summary>
    /// UIを閉じる関数
    /// </summary>
    /// <returns>UIを閉じたかどうか</returns>
    public bool CloseUI()
    {
        if (_closeStack.Peek() != null)
        {
            ReturnClosableUI().gameObject.SetActive(false);
            ReturnSelectableUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// セレクト可能UIを切り替える関数
    /// </summary>
    /// <param name="select">セレクト可能なUI</param>
    public void NextSelectableUI(ISelectable select)
    {
        _selectStack.Push(select);
    }

    /// <summary>
    /// セレクト可能UIを戻す関数
    /// </summary>
    public void ReturnSelectableUI()
    {
        if (_selectStack.Count > 0)
        {
            _selectStack.Pop();
        }
    }

    /// <summary>
    /// スロットの選択中を切り替える関数
    /// </summary>
    public void SelectedSlot()
    {
        _selectStack.Peek()?.SelectedSlot();
    }

    /// <summary>
    /// 閉じることのできるUIを切り替える関数
    /// </summary>
    /// <param name="close">閉じることのできるUI</param>
    public void NextClosableUI(IClosableUI close)
    {
        _closeStack.Push(close);
    }

    /// <summary>
    /// 閉じることのできるUIを戻す関数
    /// </summary>
    public UIBehaviour ReturnClosableUI()
    {
        if (_closeStack.Count > 0)
        {
            return (UIBehaviour)_closeStack.Pop();
        }

        return null;
    }
}
