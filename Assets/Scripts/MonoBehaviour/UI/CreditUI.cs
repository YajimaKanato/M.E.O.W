using Interface;
using UnityEngine;

public class CreditUI : UIBehaviour, IClosableUI, IUIOpenAndClose
{
    public void Close()
    {

    }

    public override bool Init(GameManager manager)
    {
        return true;
    }

    public void OpenSetting()
    {

    }
}
