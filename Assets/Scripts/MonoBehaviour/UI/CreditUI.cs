using Interface;
using UnityEngine;

public class CreditUI : UIBehaviour, IClosableUI, IUIOpenAndClose
{
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    public void Close()
    {

    }

    public void OpenSetting()
    {

    }
}
