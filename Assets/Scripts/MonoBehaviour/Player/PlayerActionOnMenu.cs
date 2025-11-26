using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionOnMenu : InitializeBehaviour
{
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager)
        {
            FailedInitialization();
        }
        else
        {
            if (!_gameManager.PlayerInputActionManager)
            {
                FailedInitialization();
            }
            else if (!_gameManager.UIManager)
            {
                FailedInitialization();
            }
            else if (!_gameManager.GameActionManager)
            {
                FailedInitialization();
            }
            else
            {
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.CancelAct, CloseMenu);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.MenuSelectAct, MenuSelectForKeyboard);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SlotNextAct, SlotNextForGamepad);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SlotBackAct, SlotBackForGamepad);
            }
        }
        return _isInitialized;
    }

    public void ChangeItemSlot()
    {

    }

    /// <summary>
    /// メニューを閉じる関数
    /// </summary>
    /// <param name="context"></param>
    void CloseMenu(InputAction.CallbackContext context)
    {
        Debug.Log("a");
        _gameManager.GameActionManager.CloseMenu();
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void MenuSelectForKeyboard(InputAction.CallbackContext context)
    {
        var key = context.control.name;
        if (key.Length > 1)
        {
            key = key.Substring(key.Length - 1);
        }
        Debug.Log(key);
        _gameManager.GameActionManager.MenuSelectForKeyboard(int.Parse(key) - 1);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotNextForGamepad(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.MenuSelectForGamepad(1);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotBackForGamepad(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.MenuSelectForGamepad(-1);
    }
}
