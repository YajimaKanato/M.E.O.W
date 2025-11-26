using Interface;
using UnityEngine;

public class Hotbar : InitializeBehaviour , ISelectable
{
    [SerializeField] ItemSlot[] _slotImages;

    int _currentSlotIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) return false;

        if (_slotImages == null) return false;

        for (int i = 0; i < _gameManager.StatusManager.PlayerRunTime.MaxSlot; i++)
        {
            if (_slotImages[i] == null) return false;
            _slotImages[i].ItemSet(null);
            _slotImages[i].SelectSign(i == 0);
        }
        return true;
    }

    /// <summary>
    /// アイテムスロットの情報を更新する関数
    /// </summary>
    /// <param name="item">更新するアイテムの情報</param>
    public void SlotUpdate(IItemBaseEffective item)
    {
        _slotImages[_currentSlotIndex].ItemSet(item != null ? item.Sprite : null);
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    public void SelectedSlot()
    {
        _preSlotIndex = _currentSlotIndex;
        _currentSlotIndex = _gameManager.StatusManager.PlayerRunTime.CurrentSlotIndex;
        _slotImages[_preSlotIndex].SelectSign(false);
        _slotImages[_currentSlotIndex].SelectSign(true);
    }
}
