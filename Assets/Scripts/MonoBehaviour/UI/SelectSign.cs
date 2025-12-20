using UnityEngine;
using UnityEngine.UI;

/// <summary>選択中表示に関する制御を行うクラス</summary>
public class SelectSign : MonoBehaviour
{
    [SerializeField, Tooltip("選択中表示のイメージ")] Image _image;

    /// <summary>
    /// 選択中の画像を設定する関数
    /// </summary>
    /// <param name="sprite">画像</param>
    public void SignSet(Sprite sprite)
    {
        if (sprite != null) _image.sprite = sprite;
    }
}
