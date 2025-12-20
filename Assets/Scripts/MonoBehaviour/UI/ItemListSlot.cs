using UnityEngine;
using UnityEngine.UI;

/// <summary>アイテムリストのスロットに関する制御を行うクラス</summary>
public class ItemListSlot : UIBehaviour
{
    [SerializeField, Tooltip("スロットに表示する画像")] Sprite[] _sprites;
    [SerializeField, Tooltip("スロットのイメージ")] Image _itemSlot;
    [SerializeField, Tooltip("選択中表示")] SelectSign _selectSign;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    /// <summary>
    /// アイテムスロットの画像を更新する関数
    /// </summary>
    /// <param name="get">アイテムを獲得しているか</param>
    public void ItemSet(bool get)
    {
        _itemSlot.sprite = _sprites[get ? 0 : 1];
    }

    /// <summary>
    /// 選択中表示を出すかどうかを変える関数
    /// </summary>
    /// <param name="active">表示を出すかどうか</param>
    /// <param name="sprite">表示の画像</param>
    public void SelectSign(bool active, Sprite sprite = null)
    {
        _selectSign.gameObject.SetActive(active);
        if (active) _selectSign.SignSet(sprite);
    }
}
