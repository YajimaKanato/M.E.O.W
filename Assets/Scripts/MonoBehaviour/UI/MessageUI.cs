using UnityEngine;
using UnityEngine.UI;

public class MessageUI : InitializeBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Image _image;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    /// <summary>
    /// テキストを更新する関数
    /// </summary>
    /// <param name="text">更新する文字列</param>
    public void TextUpdate(string text)
    {
        _text.text = text;
    }

    /// <param name="sprite">ログの背景</param>
    public void TextUISetting(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
