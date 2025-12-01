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

    Stack<ISelectableVerticalArrowUI> _selectStack;
    Stack<IClosableUI> _closeStack;
    Stack<IEnterUI> _enterStack;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        _selectStack = new Stack<ISelectableVerticalArrowUI>();
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
        NextUI(_messageUI);
    }

    /// <summary>
    /// メッセージボックスを閉じる関数
    /// </summary>
    public void MessageClose()
    {
        ReturnUI(_messageUI);
    }

    /// <summary>
    /// 会話を開始した時に呼びだす関数
    /// </summary>
    /// <param name="leftInteract">左側の会話相手の情報を持つインターフェース</param>
    /// <param name="rightInteract">右側の会話相手の情報を持つインターフェース</param>
    public void ConversationSetting(ITalkable leftInteract, ITalkable rightInteract)
    {
        NextUI(_conversationUI);
        _conversationUI.ConversationSetting(leftInteract, rightInteract);
    }

    /// <summary>
    /// 会話が終了した時に呼びだす関数
    /// </summary>
    public void ConversationEnd()
    {
        ReturnUI(_conversationUI);
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
        NextUI(_getItemUI);
        _getItemUI.GetItemUIUpdate(item?.Info, item?.Sprite);
    }

    /// <summary>
    /// アイテム獲得時のUIを閉じる関数
    /// </summary>
    public void GetItemUIClose()
    {
        ReturnUI(_getItemUI);
    }

    /// <summary>
    /// アイテム交換画面を開く関数
    /// </summary>
    public void ItemChangeOpen()
    {

    }

    /// <summary>
    /// アイテム交換画面を閉じる関数
    /// </summary>
    public void ItemChangeClose()
    {

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
            NextUI(_menuUI);
            return true;
        }
        return false;
    }

    /// <summary>
    /// スロットの選択中を切り替える関数
    /// </summary>
    public void SelectedSlot()
    {
        _selectStack.Peek()?.SelectedCategory();
    }

    #region UIに関する基本処理
    /// <summary>
    /// 指定のUIを開く関数
    /// </summary>
    /// <param name="ui">開くUI</param>
    public void OpenUI(UIBehaviour ui)
    {
        PushStack(ui, _closeStack);
        PushStack(ui, _selectStack);
        Debug.Log(_closeStack.Count);
        Debug.Log(_selectStack.Count);
        ui.gameObject.SetActive(true);
    }

    /// <summary>
    /// UIを閉じられるかを確認する関数
    /// </summary>
    /// <returns>UIを閉じたかどうか</returns>
    public bool UIClose()
    {
        if (_closeStack.Peek() != null)
        {
            CloseUI((UIBehaviour)_closeStack.Peek());
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// UIを閉じる関数
    /// </summary>
    /// <param name="ui">閉じるUI</param>
    public void CloseUI(UIBehaviour ui)
    {
        ui.gameObject.SetActive(false);
        PopStack(_closeStack);
        PopStack(_selectStack);
        Debug.Log(_closeStack.Count);
        Debug.Log(_selectStack.Count);
    }


    /// <summary>
    /// UIをスタックに登録する関数
    /// </summary>
    /// <param name="ui">登録するUI</param>
    public void NextUI(UIBehaviour ui)
    {
        ui.gameObject.SetActive(true);
        PushStack(ui, _closeStack);
        PushStack(ui, _enterStack);
        PushStack(ui, _selectStack);
        Debug.Log(_closeStack.Count);
        Debug.Log(_enterStack.Count);
        Debug.Log(_selectStack.Count);
    }

    /// <summary>
    /// UIを戻す関数
    /// </summary>
    public void ReturnUI(UIBehaviour ui)
    {
        ui.gameObject.SetActive(false);
        PopStack(_closeStack);
        PopStack(_enterStack);
        PopStack(_selectStack);
        Debug.Log(_closeStack.Count);
        Debug.Log(_enterStack.Count);
        Debug.Log(_selectStack.Count);
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
    #endregion
}
