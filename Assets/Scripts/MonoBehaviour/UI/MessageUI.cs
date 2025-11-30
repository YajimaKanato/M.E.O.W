using Interface;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : UIBehaviour, IEnterUI
{
    [SerializeField] Text _text;
    [SerializeField] Image _image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float _textSpeed = 0.1f;
    public float TextSpeed => _textSpeed;

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

    /// <summary>
    /// テキストフィールドを設定する関数
    /// </summary>
    /// <param name="index">テキストフィールドのインデックス</param>
    public void TextUISetting(int index)
    {
        _image.sprite = sprites[index];
    }
}
