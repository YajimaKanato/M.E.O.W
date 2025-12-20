using UnityEngine;
using UnityEngine.UI;

/// <summary>アイテムリスト画面のアイテム情報の表示を制御するクラス</summary>
public class ItemInfoUI : UIBehaviour
{
    [SerializeField, Tooltip("アイテムの画像を表示するイメージ")] Image _image;
    [SerializeField, Tooltip("アイテムの情報を表示するテキスト")] Text _info;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    /// <summary>
    /// アイテムの情報を設定する関数
    /// </summary>
    /// <param name="item">アイテム</param>
    public void InfoSetting(ItemInfo item)
    {
        if (item == null) return;
        _info.text = item.Info;
        _image.sprite = item.Sprite;
    }
}
