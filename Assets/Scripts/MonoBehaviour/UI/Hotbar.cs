using Interface;
using UnityEngine;

public class Hotbar : UIBehaviour, ISelectable
{
    [SerializeField] ItemSlot[] _slotImages;

    int _currentSlotIndex = 0;
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

            for (int i = 0; i < _gameManager.DataManager.Player.ItemSlot.Length; i++)
            {
                if (!_slotImages[i])
                {
                    FailedInitialization();
                    break;
                }
                _slotImages[i].ItemSet(_gameManager.DataManager.Player.ItemSlot[i].Sprite);
                _slotImages[i].SelectSign(i == 0);
            }

        }

        return _isInitialized;
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
        _currentSlotIndex = _gameManager.DataManager.PlayerRunTime.CurrentSlotIndex;
        _slotImages[_preSlotIndex].SelectSign(false);
        _slotImages[_currentSlotIndex].SelectSign(true);
    }
}
