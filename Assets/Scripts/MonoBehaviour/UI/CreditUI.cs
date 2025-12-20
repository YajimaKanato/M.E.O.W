using Interface;
using UnityEngine;

/// <summary>クレジットのUIに関する制御を行うクラス</summary>
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
