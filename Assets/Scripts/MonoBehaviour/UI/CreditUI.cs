using Interface;
using UnityEngine;

public class CreditUI : UIBehaviour, IClosableUI
{
    public override bool Init(GameManager manager)
    {
        return true;
    }
}
