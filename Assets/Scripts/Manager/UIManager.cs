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

    Stack<ISelectableUI> _selectStack;
    Stack<IClosableUI> _closeStack;
    Stack<IEnterUI> _enterStack;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        _selectStack = new Stack<ISelectableUI>();
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
                //if (ui.IsActive) NextSelectableUI(_hotbar);
                if (ui.IsActive) NextUI(_hotbar);
            }
            else if (ui.UI is ItemList)
            {
                _itemList = ui.UI as ItemList;
                if (!_itemList) FailedInitialization();
            }
            else if (ui.UI is MenuUI)
            {
                _menuUI = ui.UI as MenuUI;
                if (!_menuUI) FailedInitialization();
                //if (ui.IsActive) NextSelectableUI(_menuUI);
                if (ui.IsActive) NextUI(_menuUI);
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
        //NextSelectableUI(null);
        //NextClosableUI(null);
        NextUI(_messageUI);
    }

    /// <summary>
    /// メッセージボックスを閉じる関数
    /// </summary>
    public void MessageClose()
    {
        _messageUI.gameObject.SetActive(false);
        //ReturnSelectableUI();
        //ReturnClosableUI();
        ReturnUI();
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
    /// エンター入力時に現在のUIを判定する関数
    /// </summary>
    /// <returns>メッセージのUIかどうか</returns>
    public bool PushEnter()
    {
        return _enterStack.Peek() is MessageUI;
    }

    /// <summary>
    /// エンター入力時に行う関数
    /// </summary>
    public void PushEnterAction()
    {
        _enterStack.Peek()?.PushEnter();
    }

    /// <summary>
    /// テキストを読み飛ばす関数
    /// </summary>
    /// <returns>読み飛ばしたかどうか</returns>
    public bool PushEnterTextUpdate()
    {
        return _messageUI.IsTyping;
    }

    /// <summary>
    /// テキストに関する表示を更新する関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void MessageTextUpdate(string text, int index)
    {
        _messageUI.TextUpdate(text);
        _messageUI.TextUISetting(index);
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
            //NextSelectableUI(_menuUI);
            //NextClosableUI(_menuUI);
            NextUI(_menuUI);
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
            ((UIBehaviour)_closeStack.Peek()).gameObject.SetActive(false);
            //ReturnSelectableUI();
            ReturnUI();
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
    public void NextSelectableUI(ISelectableUI select)
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

    /// <summary>
    /// エンターに反応するUIを切り替える関数
    /// </summary>
    /// <param name="enter"></param>
    public void NextEnterableUI(IEnterUI enter)
    {
        _enterStack.Push(enter);
    }

    /// <summary>
    /// エンターに反応するUIを戻す関数
    /// </summary>
    public void ReturnEnterableUI()
    {
        if (_enterStack.Count > 0)
        {
            _enterStack.Pop();
        }
    }

    /// <summary>
    /// UIをスタックに登録する関数
    /// </summary>
    /// <param name="ui">登録するUI</param>
    public void NextUI(UIBehaviour ui)
    {
        PushStack(ui, _closeStack);
        PushStack(ui, _enterStack);
        PushStack(ui, _selectStack);
        Debug.Log(_closeStack.Peek());
        Debug.Log(_enterStack.Peek());
        Debug.Log(_selectStack.Peek());
    }

    /// <summary>
    /// UIを戻す関数
    /// </summary>
    public void ReturnUI()
    {
        PopStack(_closeStack);
        PopStack(_enterStack);
        PopStack(_selectStack);
    }

    /// <summary>
    /// 任意のスタックに登録する関数
    /// </summary>
    /// <typeparam name="T">登録するスタックの型</typeparam>
    /// <param name="ui">登録するUI</param>
    /// <param name="stack">スタック</param>
    public void PushStack<T>(UIBehaviour ui, Stack<T> stack) where T : IUIBase
    {
        if (ui is T)
        {
            stack.Push((T)(object)ui);
        }
        else
        {
            stack.Push(default);
        }
    }

    /// <summary>
    /// スタックからポップする関数
    /// </summary>
    /// <typeparam name="T">スタックの型</typeparam>
    /// <param name="stack">スタック</param>
    public void PopStack<T>(Stack<T> stack) where T : IUIBase
    {
        stack.Pop();
    }
}
