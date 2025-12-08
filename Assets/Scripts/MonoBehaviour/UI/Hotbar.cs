using Interface;
using UnityEngine;

public class Hotbar : UIBehaviour, ISelectableNumberUIForKeyboard, ISelectableNumberUIForGamepad
{
    [SerializeField] HotbarData _data;
    [SerializeField] ItemSlot[] _slotImages;
    HotbarRunTime _hotbarRunTime;
    int _currentIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        _runtimeDataManager.RegisterData(_id, new HotbarRunTime(_data));
        InitializeManager.InitializationForVariable(out _hotbarRunTime, _runtimeDataManager.GetData<HotbarRunTime>(_id));
        if (_isInitialized)
        {
            if (_slotImages == null) InitializeManager.FailedInitialization();

            //アイテムスロットの初期化
            var slot = _hotbarRunTime.ItemSlot;
            var slotLength = slot.Length;
            for (int i = 0; i < slotLength; i++)
            {
                if (!_slotImages[i])
                {
                    InitializeManager.FailedInitialization();
                    break;
                }
                _slotImages[i].ItemSet(slot[i]?.Sprite);
                _slotImages[i].SelectSign(i == 0);
            }
        }

        return _isInitialized;
    }

    /// <summary>
    /// アイテムスロットの情報を更新する関数
    /// </summary>
    /// <param name="sprite">更新するアイテムの情報</param>
    public void SlotUpdate(Sprite sprite, int index)
    {
        _slotImages[index != -1 ? index : _currentIndex].ItemSet(sprite);
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    /// <param name="index">切り替えるインデックス</param>
    void ISelectableNumberUIForGamepad.SelectedCategory(int index)
    {
        _hotbarRunTime.SelectItemForGamepad(index);
        SelectUpdate(index);
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    /// <param name="index">切り替えるインデックス</param>
    void ISelectableNumberUIForKeyboard.SelectedCategory(int index)
    {
        _hotbarRunTime.SelectItemForKeyboard(index);
        SelectUpdate(index);
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    /// <param name="index">切り替えるインデックス</param>
    void SelectUpdate(int index)
    {
        _preSlotIndex = _currentIndex;
        _currentIndex = _hotbarRunTime.CurrentSlotIndex;
        _slotImages[_preSlotIndex].SelectSign(false);
        _slotImages[_currentIndex].SelectSign(true);
    }
}
