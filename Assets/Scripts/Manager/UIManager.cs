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
    ISelectable _currentSelect;

    bool _isEnter = false;
    bool _isTyping = false;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        _selectStack = new Stack<ISelectable>();

        foreach (var ui in _uiSettings)
        {
            if (ui.UI is ConversationUI)
            {
                _conversationUI = ui.UI as ConversationUI;
            }
            else if (ui.UI is MessageUI)
            {
                _messageUI = ui.UI as MessageUI;
            }
            else if (ui.UI is GetItemUI)
            {
                _getItemUI = ui.UI as GetItemUI;
            }
            else if (ui.UI is Hotbar)
            {
                _hotbar = ui.UI as Hotbar;
                if (_hotbar is ISelectable && ui.IsActive) _currentSelect = _hotbar;
            }
            else if (ui.UI is ItemList)
            {
                _itemList = ui.UI as ItemList;
            }else if(ui.UI is MenuUI)
            {
                _menuUI = ui.UI as MenuUI;
                if (_menuUI is ISelectable && ui.IsActive) _currentSelect = _menuUI;
            }
            ui.UI.Init(manager);
            ui.UI.gameObject.SetActive(ui.IsActive);
        }

        if (!_conversationUI) FailedInitialization();
        if (!_messageUI) FailedInitialization();
        if (!_getItemUI) FailedInitialization();
        if (!_hotbar) FailedInitialization();
        if (!_itemList) FailedInitialization();
        return _isInitialized;
    }

    /// <summary>
    /// メッセージボックスを表示する関数
    /// </summary>
    public void MessageOpen()
    {
        _messageUI.gameObject.SetActive(true);
        NextSelectableUI(null);
    }

    /// <summary>
    /// メッセージボックスを閉じる関数
    /// </summary>
    public void MessageClose()
    {
        _messageUI.gameObject.SetActive(false);
        ReturnSelectableUI();
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
    /// テキストフィールドを設定する関数
    /// </summary>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void TextFieldSetting(int index)
    {
        _messageUI.TextUISetting(index);
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
    /// セレクト可能UIを切り替える関数
    /// </summary>
    /// <param name="select">セレクト可能なUI</param>
    public void NextSelectableUI(ISelectable select)
    {
        _selectStack.Push(_currentSelect);
        _currentSelect = select;
        Debug.Log(_currentSelect);
    }

    /// <summary>
    /// セレクト可能UIを戻す関数
    /// </summary>
    public void ReturnSelectableUI()
    {
        if (_selectStack.Count > 0)
        {
            _currentSelect = _selectStack.Pop();
        }
    }

    /// <summary>
    /// スロットの選択中を切り替える関数
    /// </summary>
    public void SelectedSlot()
    {
        _currentSelect?.SelectedSlot();
    }

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

    /// <summary>
    /// メニューを開く関数
    /// </summary>
    public void OpenMenu()
    {
        NextSelectableUI(_menuUI);
        _menuUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// メニューを閉じる関数
    /// </summary>
    public void CloseMenu()
    {
        _menuUI.gameObject.SetActive(false);
        ReturnSelectableUI();
    }
}
