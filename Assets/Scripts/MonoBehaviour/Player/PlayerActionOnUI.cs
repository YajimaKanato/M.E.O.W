using UnityEngine;

[System.Serializable]
public class PlayerActionOnUI : InitializeBehaviour
{
    public void ChangeItemSlot()
    {

    }

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) return false;
        return true;
    }
}
