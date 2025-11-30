using Interface;
using UnityEngine;

public class Hotbar : UIBehaviour, ISelectable
{
    [SerializeField] ItemSlot[] _slotImages;

    int _currentIndex = 0;
    int _preSlotIndex = 0;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager)
        {
            FailedInitialization();
        }
        else
        {
            if (_slotImages == null) FailedInitialization();

            //アイテムスロットの初期化
            var slot = _gameManager.DataManager.HotbarRunTime.ItemSlot;
            var slotLength = slot.Length;
            for (int i = 0; i < slotLength; i++)
            {
                if (!_slotImages[i])
                {
                    FailedInitialization();
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
    public void SelectedSlot()
    {
        _preSlotIndex = _currentIndex;
        _currentIndex = _gameManager.DataManager.HotbarRunTime.CurrentSlotIndex;
        _slotImages[_preSlotIndex].SelectSign(false);
        _slotImages[_currentIndex].SelectSign(true);
    }
}
