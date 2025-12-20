using Interface;
using UnityEngine;

public class GiveAnyItemUI : UIBehaviour, IEnterUI, IClosableUI, ISelectableNumberUIForGamepad, ISelectableNumberUIForKeyboard
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

    public void PushEnter()
    {

    }

    void ISelectableNumberUIForGamepad.SelectedCategory(int index)
    {

    }

    void ISelectableNumberUIForKeyboard.SelectedCategory(int index)
    {

    }
}
