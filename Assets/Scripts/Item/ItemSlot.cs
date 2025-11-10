using UnityEngine;
using System.Collections.Generic;
using Interface;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] ItemList _itemList;
    IItemBaseEffective[] _itemSlot = new IItemBaseEffective[6];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SlotSet()
    {

    }

    /// <summary>
    /// アイテムスロットの情報を更新する関数
    /// </summary>
    public void SlotUpdate()
    {

    }

    /// <summary>
    /// アイテムスロットのアイテムセレクトをする関数
    /// </summary>
    /// <param name="nextIndex">次に選ぶアイテムのインデックス</param>
    /// <returns>選んだアイテム</returns>
    public IItemBaseEffective SelectItem(int nextIndex)
    {
        return _itemSlot[nextIndex];
    }
}
