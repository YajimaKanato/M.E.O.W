using Interface;

/// <summary>UIに関する制御を行うクラス</summary>
public class UIManager : UIManagerBase
{
    DataManager _dataManager;
    MessageRunTime _messageRunTime;
    ConversationUI _conversationUI;
    MessageUI _messageUI;
    GetItemUI _getItemUI;
    Hotbar _hotbar;
    ItemList _itemList;
    MenuUI _menuUI;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _dataManager, _gameManager.DataManager);
        InitializeManager.InitializationForVariable(out _messageRunTime, _dataManager.MessageRunTime);

        foreach (var ui in _uiSettings)
        {
            if (ui.UI is ConversationUI)
            {
                InitializeManager.InitializationForVariable(out _conversationUI, ui.UI as ConversationUI);
            }
            else if (ui.UI is MessageUI)
            {
                InitializeManager.InitializationForVariable(out _messageUI, ui.UI as MessageUI);
            }
            else if (ui.UI is GetItemUI)
            {
                InitializeManager.InitializationForVariable(out _getItemUI, ui.UI as GetItemUI);
            }
            else if (ui.UI is Hotbar)
            {
                InitializeManager.InitializationForVariable(out _hotbar, ui.UI as Hotbar);
            }
            else if (ui.UI is ItemList)
            {
                InitializeManager.InitializationForVariable(out _itemList,ui.UI as ItemList);
            }
            else if (ui.UI is MenuUI)
            {
                InitializeManager.InitializationForVariable(out _menuUI, ui.UI as MenuUI);
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
        _messageRunTime.TextFieldSetting(text, index);
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
