using Interface;
using UnityEngine;

public class TitleSelect : UIBehaviour, ISelectUI
{
    [SerializeField] GameObject _selectSign;

    public void SelectSign(bool active)
    {
        _selectSign.SetActive(active);
    }
}
