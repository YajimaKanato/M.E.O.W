using UnityEngine;
using Interface;

public class Hotbar : MonoBehaviour
{
    [SerializeField] ItemSlot[] _slotImages;
    GameActionManager _gameActionManager;

    static IItemBaseEffective[] _itemSlot;

    int _currentSlotIndex = 0;
    int _preSlotIndex = 0;
    const int MAXSLOT = 6;
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {
        _gameActionManager = GameActionManager.Instance;
        _itemSlot = new IItemBaseEffective[MAXSLOT];
        for (int i = 0; i < MAXSLOT; i++)
        {
            _slotImages[i].ItemSet(_itemSlot[i] != null ? _itemSlot[i].Sprite : null);
            _slotImages[i].SelectSign(i == _currentSlotIndex);
        }
    }

    /// <summary>
    /// アイテムを獲得する関数
    /// </summary>
    /// <param name="item">獲得するアイテム</param>
    public void GetItem(IItemBase item)
    {
        for (int i = 0; i < MAXSLOT; i++)
        {
            if (_itemSlot[i] == null)
            {
                _itemSlot[i] = (IItemBaseEffective)item;
                SlotUpdate(_slotImages[i], _itemSlot[i]);
                return;
            }
        }

        //アイテムスロットいっぱいの時の処理
    }

    /// <summary>
    /// アイテムスロットの情報を更新する関数
    /// </summary>
    /// <param name="slot">更新するスロット</param>
    /// <param name="item">更新するアイテムの情報</param>
    public void SlotUpdate(ItemSlot slot, IItemBaseEffective item)
    {
        slot.ItemSet(item.Sprite);
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    public void SelectedSlot()
    {
        _slotImages[_preSlotIndex].SelectSign(false);
        _slotImages[_currentSlotIndex].SelectSign(true);
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForKeyboard(int index)
    {
        _preSlotIndex = _currentSlotIndex;
        _currentSlotIndex = index;
        Debug.Log($"Select : {_currentSlotIndex} => " + (_itemSlot[_currentSlotIndex] != null ? _itemSlot[_currentSlotIndex].ItemType : "null"));
        SelectedSlot();
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForGamepad(int index)
    {
        _preSlotIndex = _currentSlotIndex;
        _currentSlotIndex += index;
        //行き止まり
        //if (_currentSlotIndex >= MAXSLOT)
        //{
        //    _currentSlotIndex = MAXSLOT - 1;
        //}
        //if (_currentSlotIndex <= 0)
        //{
        //    _currentSlotIndex = 0;
        //}

        //ループ
        if (_currentSlotIndex >= MAXSLOT)
        {
            _currentSlotIndex = 0;
        }
        if (_currentSlotIndex < 0)
        {
            _currentSlotIndex = MAXSLOT - 1;
        }
        Debug.Log($"Select : {_currentSlotIndex}");
        SelectedSlot();
    }


    /// <summary>
    /// アイテムを使用するときに呼ばれる関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    public void UseItem(PlayerInfo player)
    {
        if (_itemSlot[_currentSlotIndex] == null)
        {
            Debug.Log("Command Invalid");
        }
        else
        {
            _gameActionManager.ItemActivate(_itemSlot[_currentSlotIndex], player);
            _itemSlot[_currentSlotIndex] = null;
            SlotUpdate(_slotImages[_currentSlotIndex], _itemSlot[_currentSlotIndex]);
        }
    }
}
