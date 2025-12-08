using Interface;
using Item;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>UIに関する制御を行うクラス</summary>
public class UIManager : UIManagerBase
{
    [SerializeField] UIDataList _uiDataList;
    ConversationUI _conversationUI;
    MessageUI _messageUI;
    GetItemUI _getItemUI;
    Hotbar _hotbarUI;
    ItemList _itemList;
    ChangeItemUI _changeItemUI;
    MenuUI _menuUI;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        //UIの初期化
        if (!_uiDataList || !_uiDataList.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();

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
                InitializeManager.InitializationForVariable(out _hotbarUI, ui.UI as Hotbar);
            }
            else if (ui.UI is ItemList)
            {
                InitializeManager.InitializationForVariable(out _itemList, ui.UI as ItemList);
            }
            else if (ui.UI is MenuUI)
            {
                InitializeManager.InitializationForVariable(out _menuUI, ui.UI as MenuUI);
            }
            else if (ui.UI is ChangeItemUI)
            {
                InitializeManager.InitializationForVariable(out _changeItemUI, ui.UI as ChangeItemUI);
            }
            ui.UI?.Init(manager);
            if (ui.IsActive) _uiStack.Push((IUIBase)ui.UI);
            ui.UI?.gameObject.SetActive(ui.IsActive);
        }
        return _isInitialized;
    }

    /// <summary>
    /// メッセージを流している途中かどうかを返す関数
    /// </summary>
    /// <returns>メッセージを流している途中かどうか</returns>
    public bool IsTyping()
    {
        return _runtimeDataManager.GetData<MessageRunTime>(_messageUI.ID).IsTyping;
    }

    #region UIに関する詳細な処理
    /// <summary>
    /// テキストに関する表示を更新する関数
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void MessageTextUpdate(string text, int index)
    {
        _runtimeDataManager.GetData<MessageRunTime>(_messageUI.ID).TextFieldSetting(text, index);
        _messageUI.MessageUpdate();
    }

    /// <summary>
    /// アイテムに応じてスロットの表示を切り替える関数
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <param name="index">更新するスロット（交換時には-1以外を指定）</param>
    public void SlotUpdate(UsableItem item, int index = -1)
    {
        _hotbarUI.SlotUpdate(item?.Sprite, index != -1 ? _runtimeDataManager.GetData<ChangeItemRunTime>(_changeItemUI.ID).CurrentSlotIndex : index);
    }

    /// <summary>
    /// 会話に関する設定
    /// </summary>
    /// <param name="left">左側に表示するキャラクターのID</param>
    /// <param name="right">右側に表示するキャラクターのID</param>
    public void ConversationSetting(ITalkable left, ITalkable right)
    {
        _runtimeDataManager.GetData<ConversationRunTime>(_conversationUI.ID).CharacterDataSetting(left, right);
    }

    /// <summary>
    /// アイテムを獲得した時に行う関数
    /// </summary>
    /// <param name="item">獲得したアイテム</param>
    /// <returns>アイテムを格納したインデックス</returns>
    public int GetItem(ItemInfo item)
    {
        _runtimeDataManager.GetData<GetItemRunTime>(_getItemUI.ID).GetItemSetting(item.Sprite, item.Info);
        if (item.ItemRole == ItemRole.KeyItem)
        {
            //アイテムリスト
            Debug.Log($"Get => {item}");
            return -2;
        }
        else
        {
            return _runtimeDataManager.GetData<HotbarRunTime>(_hotbarUI.ID).GetItem((UsableItem)item);
        }
    }

    /// <summary>
    /// アイテムを交換する関数
    /// </summary>
    /// <param name="item">交換するアイテム</param>
    /// <returns>交換したアイテム</returns>
    public UsableItem ItemChange(UsableItem item)
    {
        return _runtimeDataManager.GetData<HotbarRunTime>(_hotbarUI.ID).ChangeItem(item, _runtimeDataManager.GetData<ChangeItemRunTime>(_changeItemUI.ID).CurrentSlotIndex);
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
        OpenUI(_changeItemUI);
    }

    /// <summary>
    /// メッセージのUIを開く関数
    /// </summary>
    /// <param name="message">表示するテキスト</param>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void OpenMessage(string message, int index)
    {
        OpenUI(_messageUI);
        MessageTextUpdate(message, index);
    }

    /// <summary>
    /// 会話時のUIを開く関数
    /// </summary>
    /// <param name="left">左側に表示するキャラクターのデータ</param>
    /// <param name="right">右側に表示するキャラクターのデータ</param>
    public void OpenConversation(ITalkable left, ITalkable right)
    {
        ConversationSetting(left, right);
        OpenUI(_conversationUI);
    }

    public void OpenGetItem()
    {
        OpenUI(_getItemUI);
    }
    #endregion
}
