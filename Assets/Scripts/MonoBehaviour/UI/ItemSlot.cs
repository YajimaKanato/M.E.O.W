using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : UIBehaviour
{
    [SerializeField] Image _itemSlot;
    [SerializeField] GameObject _selectSign;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    /// <summary>
    /// アイテムスロットの画像を更新する関数
    /// </summary>
    /// <param name="image">スロットに表示する画像（nullも可）</param>
    public void ItemSet(Sprite image)
    {
        _itemSlot.sprite = image;
    }

    /// <summary>
    /// 選択中表示を出すかどうかを変える関数
    /// </summary>
    /// <param name="active">表示を出すかどうか</param>
    public void SelectSign(bool active)
    {
        _selectSign.SetActive(active);
    }
}
