using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionOnUI : InitializeBehaviour
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
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.MenuSelectActOnUI, MenuSelectForKeyboard);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SlotNextActOnUI, SlotNextForGamepad);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SlotBackActOnUI, SlotBackForGamepad);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.EnterActOnUI, PushEnter);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.CancelActOnUI, PushCancel);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.MenuActOnUI, MenuOpen);
            }
        }
        return _isInitialized;
    }

    /// <summary>
    /// メニューを開く関数
    /// </summary>
    /// <param name="context"></param>
    void MenuOpen(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.OpenMenu();
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
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
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotNextForGamepad(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.MenuSelectForGamepad(1);
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotBackForGamepad(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.MenuSelectForGamepad(-1);
    }

    /// <summary>
    /// エンターを押したときに行う関数
    /// </summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.PushEnterUntilTalking();
    }

    /// <summary>
    /// キャンセルボタンをおしたときに行う関数
    /// </summary>
    /// <param name="context"></param>
    void PushCancel(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.CloseUI();
    }
}
