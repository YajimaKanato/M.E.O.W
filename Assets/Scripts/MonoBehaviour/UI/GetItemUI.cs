using UnityEngine;
using UnityEngine.UI;

public class GetItemUI : InitializeBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Text _text;

    /// <summary>
    /// 獲得したアイテムに関する表示をする関数
    /// </summary>
    /// <param name="info">アイテムの情報</param>
    /// <param name="sprite">アイテムの画像</param>
    public void GetItemUIUpdate(string info, Sprite sprite)
    {
        _image.sprite = sprite;
        _text.text = info;
    }

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
