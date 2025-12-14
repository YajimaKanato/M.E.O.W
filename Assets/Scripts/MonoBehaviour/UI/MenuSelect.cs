using Interface;
using UnityEngine;

public class MenuSelect : UIBehaviour, IUIOpenAndClose
{
    public virtual void Close()
    {

    }

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    public virtual void OpenSetting()
    {

    }
}
