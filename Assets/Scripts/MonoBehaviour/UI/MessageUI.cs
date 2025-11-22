using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Image _image;

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
