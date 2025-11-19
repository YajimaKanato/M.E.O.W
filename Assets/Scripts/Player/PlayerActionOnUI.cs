using UnityEngine;

public class PlayerActionOnUI : MonoBehaviour
{
    PlayerInputActionManager _playerInputActions;
    GameActionManager _gameActionManager;

    private void Awake()
    {
        _playerInputActions = PlayerInputActionManager.Instance;
        _gameActionManager = GameActionManager.Instance;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void ChangeItemSlot()
    {

    }
}
