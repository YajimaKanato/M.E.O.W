using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MessageUI : InitializeBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Image _image;

    public override bool Init(GameManager manager)
    {
        return true;
    }

    /// <summary>
    /// テキストを更新する関数
    /// </summary>
    /// <param name="text">更新する文字列</param>
    /// <param name="sprite">ログの背景</param>
    public void TextUpdate(string text, Sprite sprite)
    {
        _image.sprite = sprite;
        _text.text = text;
    }
}
