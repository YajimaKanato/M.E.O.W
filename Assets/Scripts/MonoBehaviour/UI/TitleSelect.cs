using UnityEngine;
using UnityEngine.UI;

/// <summary>タイトル画面のセレクトに関する制御を行うクラス</summary>
public class TitleSelect : UIBehaviour
{
    [SerializeField, Tooltip("セレクト中の画像")] Sprite _selectSprite;
    [SerializeField, Tooltip("セレクトしてないときの画像")] Sprite _unselectSprite;
    [SerializeField, Tooltip("画像を貼るイメージ")] Image _image;
    [SerializeField, Tooltip("選択中表示")] SelectSign _selectSign;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    /// <summary>
    /// 選択中表示を切り替える関数
    /// </summary>
    /// <param name="active">選択中表示をするかどうか</param>
    public void SelectSign(bool active)
    {
        _selectSign.gameObject.SetActive(active);
        _image.sprite = active ? _selectSprite : _unselectSprite;
    }
}
