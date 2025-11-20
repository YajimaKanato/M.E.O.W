using UnityEngine;
using Interface;

public class Hotbar : MonoBehaviour
{
    [SerializeField] ItemSlot[] _slotImages;
    GameActionManager _gameActionManager;

    static IItemBaseEffective[] _itemSlot;

    int _slotIndex = 0;
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
        SelectedSlot();
        for (int i = 0; i < MAXSLOT; i++)
        {
            _slotImages[i].ItemSet(_itemSlot[i] != null ? _itemSlot[i].Sprite : null);
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
        for (int i = 0; i < MAXSLOT; i++)
        {
            if (i == _slotIndex)
            {
                _slotImages[i].SelectSign(true);
            }
            else
            {
                _slotImages[i].SelectSign(false);
            }
        }
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForKeyboard(int index)
    {
        _slotIndex = index;
        Debug.Log($"Select : {_slotIndex} => {_itemSlot[_slotIndex]}");
        SelectedSlot();
    }

    /// <summary>
    /// アイテムセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectItemForGamepad(int index)
    {
        _slotIndex += index;
        if (_slotIndex >= MAXSLOT)
        {
            _slotIndex = MAXSLOT;
        }
        if (_slotIndex <= 0)
        {
            _slotIndex = 0;
        }
        Debug.Log($"Select : {_slotIndex}");
        SelectedSlot();
    }


    /// <summary>
    /// アイテムを使用するときに呼ばれる関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    public void UseItem(PlayerInfo player)
    {
        if (_itemSlot[_slotIndex] == null)
        {
            Debug.Log("Command Invalid");
        }
        else
        {
            _gameActionManager.ItemActivate(_itemSlot[_slotIndex], player);
            _itemSlot[_slotIndex] = null;
            SlotUpdate(_slotImages[_slotIndex], _itemSlot[_slotIndex]);
        }
    }
}
