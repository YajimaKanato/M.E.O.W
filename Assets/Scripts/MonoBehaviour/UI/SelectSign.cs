using UnityEngine;
using UnityEngine.UI;

public class SelectSign : MonoBehaviour
{
    [SerializeField] Image _image;

    /// <summary>
    /// 選択中の画像を設定する関数
    /// </summary>
    /// <param name="sprite">画像</param>
    public void SignSet(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
