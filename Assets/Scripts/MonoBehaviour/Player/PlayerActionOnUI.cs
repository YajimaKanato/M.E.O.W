using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionOnUI : InitializeBehaviour
{
    PlayerInputActionManager _playerInputActionManager;
    GameActionManager _gameActionManager;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _playerInputActionManager, _gameManager.PlayerInputActionManager);
        InitializeManager.InitializationForVariable(out _gameActionManager, _gameManager.GameActionManager);
        if (_isInitialized)
        {
            if (!_playerInputActionManager)
            {
                InitializeManager.FailedInitialization();
            }
            else if (!_gameActionManager)
            {
                InitializeManager.FailedInitialization();
            }
            else
            {
                _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuSelectActOnUI, MenuSelectForKeyboard);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.ItemSlotActOnUI, SlotSelectForKeyboard);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.SlotNextActOnUI, SlotNextForGamepad);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.SlotBackActOnUI, SlotBackForGamepad);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.EnterActOnUI, PushEnter);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.CancelActOnUI, PushCancel);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuActOnUI, MenuOpen);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectUpOnUI, UpArrow);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectDownOnUI, DownArrow);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectRightOnUI, RightArrow);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectLeftOnUI, LeftArrow);
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
        _gameActionManager.OpenMenu();
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
        _gameActionManager.SelectForKeyboard(int.Parse(key) - 1);
    }

    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotSelectForKeyboard(InputAction.CallbackContext context)
    {
        var key = context.control.name;
        if (key.Length > 1)
        {
            key = key.Substring(key.Length - 1);
        }
        Debug.Log(key);
        _gameActionManager.SelectForKeyboard(int.Parse(key) - 1);
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotNextForGamepad(InputAction.CallbackContext context)
    {
        _gameActionManager.SelectForGamepad(1);
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotBackForGamepad(InputAction.CallbackContext context)
    {
        _gameActionManager.SelectForGamepad(-1);
    }

    /// <summary>
    /// エンターを押したときに行う関数
    /// </summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        _gameActionManager.PushEnter();
    }

    /// <summary>
    /// キャンセルボタンをおしたときに行う関数
    /// </summary>
    /// <param name="context"></param>
    void PushCancel(InputAction.CallbackContext context)
    {
        _gameActionManager.CloseUI();
    }

    void UpArrow(InputAction.CallbackContext context)
    {
        _gameActionManager.SelectVerticalArrow(-1);
    }

    void DownArrow(InputAction.CallbackContext context)
    {
        _gameActionManager.SelectVerticalArrow(1);
    }

    void RightArrow(InputAction.CallbackContext context)
    {
        _gameActionManager.SelectHorizontalArrow(1);
    }

    void LeftArrow(InputAction.CallbackContext context)
    {
        _gameActionManager.SelectHorizontalArrow(-1);
    }
}
