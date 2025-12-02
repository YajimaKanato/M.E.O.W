using UnityEngine;
using UnityEngine.UI;

public class TitleSelect : UIBehaviour
{
    [SerializeField] Sprite _selectSprite;
    [SerializeField] Sprite _unselectSprite;
    [SerializeField] Image _image;
    [SerializeField] GameObject _selectSign;

    public void SelectSign(bool active)
    {
        _selectSign.SetActive(active);
        _image.sprite = active ? _selectSprite : _unselectSprite;
    }
}
