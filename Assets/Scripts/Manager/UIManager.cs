using Interface;

/// <summary>UIに関する制御を行うクラス</summary>
public class UIManager : UIManagerBase
{
    ConversationUI _conversationUI;
    MessageUI _messageUI;
    GetItemUI _getItemUI;
    Hotbar _hotbar;
    ItemList _itemList;
    MenuUI _menuUI;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();

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
            }
            ui.UI?.Init(manager);
            if (ui.IsActive) _uiStack.Push((IUIBase)ui.UI);
            ui.UI?.gameObject.SetActive(ui.IsActive);
        }
        return _isInitialized;
    }
    #region UIに関する詳細な処理
    /// <summary>
    /// テキストに関する表示を更新する関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void MessageTextUpdate(string text, int index)
    {
        _gameManager.DataManager.MessageRunTime.TextFieldSetting(text, index);
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
    #endregion

    #region UIを開く
    /// <summary>
    /// メニューを開けるかを確認する関数
    /// </summary>
    public bool OpenMenu()
    {
        return OpenUI(_menuUI);
    }

    /// <summary>
    /// アイテム交換画面を開く関数
    /// </summary>
    public void OpenItemChange()
    {

    }

    /// <summary>
    /// メッセージのUIを開く関数
    /// </summary>
    public void OpenMessage()
    {
        OpenUI(_messageUI);
    }

    /// <summary>
    /// 会話時のUIを開く関数
    /// </summary>
    public void OpenConversation()
    {
        OpenUI(_conversationUI);
    }

    public void OpenGetItem()
    {
        OpenUI(_getItemUI);
    }
    #endregion
}
