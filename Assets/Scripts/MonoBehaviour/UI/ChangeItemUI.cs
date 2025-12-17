using Interface;
using UnityEngine;

/// <summary>アイテム交換画面のUIに関する制御を行うクラス</summary>
public class ChangeItemUI : UIBehaviour, ISelectableNumberUIForKeyboard, ISelectableNumberUIForGamepad, IClosableUI, IEnterUI
{
    [SerializeField, Tooltip("ホットバーのデータ")] HotbarData _data;
    [SerializeField, Tooltip("アイテム交換画面のスロット")] ItemSlot[] _slotImages;
    UIManager _uiManager;
    ChangeItemRunTime _changeItemRunTime;
    int _currentIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        //ランタイムデータ
        _runtimeDataManager.RegisterData(_id, new ChangeItemRunTime(_data));
        _isInitialized = InitializeManager.InitializationForVariable(out _changeItemRunTime, _runtimeDataManager.GetData<ChangeItemRunTime>(_id));
        if (_isInitialized)
        {
            if (_slotImages == null) _isInitialized = InitializeManager.FailedInitialization();
        }
        return _isInitialized;
    }

    public void Close()
    {
        _uiManager.NotItemChange();
    }

    public void OpenSetting()
    {
        //アイテムスロットの初期化
        var slot = _changeItemRunTime.ItemSlot;
        var slotLength = slot.Length;
        for (int i = 0; i < slotLength; i++)
        {
            _slotImages[i].ItemSet(slot[i]?.Sprite);
            _slotImages[i].SelectSign(i == _changeItemRunTime.CurrentSlotIndex, _runtimeDataManager.GetData<ChangeItemRunTime>(_id).ChangeItem.Sprite);
        }
    }

    public void PushEnter()
    {
        _uiManager.ItemChange();
    }

    void ISelectableNumberUIForKeyboard.SelectedCategory(int index)
    {
        _changeItemRunTime.SelectItemForKeyboard(index);
        SelectUpdate();
    }

    void ISelectableNumberUIForGamepad.SelectedCategory(int index)
    {
        _changeItemRunTime.SelectItemForGamepad(index);
        SelectUpdate();
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    void SelectUpdate()
    {
        _preSlotIndex = _currentIndex;
        _currentIndex = _changeItemRunTime.CurrentSlotIndex;
        _slotImages[_preSlotIndex].SelectSign(false);
        _slotImages[_currentIndex].SelectSign(true, _runtimeDataManager.GetData<ChangeItemRunTime>(_id).ChangeItem.Sprite);
    }
}
