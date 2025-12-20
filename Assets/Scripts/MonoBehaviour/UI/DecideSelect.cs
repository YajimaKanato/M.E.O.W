using UnityEngine;

/// <summary>決定する意思を表示するUIに関する制御を行うクラス</summary>
public class DecideSelect : MonoBehaviour
{
    [SerializeField, Tooltip("選択中表示")] SelectSign _selectSign;

    /// <summary>
    /// 選択中表示を出すかどうかを変える関数
    /// </summary>
    /// <param name="active">表示を出すかどうか</param>
    /// <param name="sprite">表示の画像</param>
    public void SelectSign(bool active, Sprite sprite = null)
    {
        _selectSign.gameObject.SetActive(active);
        if (active) _selectSign.SignSet(sprite);
    }
}
