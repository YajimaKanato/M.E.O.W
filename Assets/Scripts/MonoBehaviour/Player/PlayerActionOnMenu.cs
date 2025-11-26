using UnityEngine;

public class PlayerActionOnMenu : InitializeBehaviour
{
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        return _isInitialized;
    }

    public void ChangeItemSlot()
    {

    }
}
