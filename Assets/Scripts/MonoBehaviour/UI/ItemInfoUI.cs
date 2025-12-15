using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : UIBehaviour
{
    [SerializeField] Text _info;
    [SerializeField] Image _image;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    public void InfoSetting(ItemInfo item)
    {
        if (item == null) return;
        _info.text = item.Info;
        _image.sprite = item.Sprite;
    }
}
