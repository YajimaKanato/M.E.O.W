using Interface;
using UnityEngine;

public class Hotbar : InitializeBehaviour
{
    [SerializeField] ItemSlot[] _slotImages;

    int _currentSlotIndex = 0;
    int _preSlotIndex = 0;

    public override void Init(GameManager manager)
    {
        _gameManager = manager;
        for (int i = 0; i < _gameManager.StatusManager.PlayerRunTime.MaxSlot; i++)
        {
            _slotImages[i].ItemSet(null);
            _slotImages[i].SelectSign(i == 0);
        }
        Debug.Log($"{this} has Initialized");
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
