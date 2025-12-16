using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>アウトゲームのプレイヤーの動きに関する制御を行うクラス</summary>
public class PlayerActionOnOutGame : InitializeBehaviour
{
    PlayerInputActionManager _playerInputActionManager;
    OutGameActionManager _outGameActionManager;
    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _playerInputActionManager, _gameManager.PlayerInputActionManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _outGameActionManager, _gameManager.OutGameActionManager);
        if (_isInitialized)
        {
            _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuNextActOnOutGame, MenuNext);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuBackActOnOutGame, MenuBack);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuSelectActOnOutGame, MenuSelect);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.EnterActOnOutGame, PushEnter);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.CancelActOnOutGame, PushCancel);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectUpOnOutGame, SelectUp);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectDownOnOutGame, SelectDown);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectRightOnOutGame, SelectRight);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectLeftOnOutGame, SelectLeft);
        }
        return _isInitialized;
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void MenuNext(InputAction.CallbackContext context)
    {
        _outGameActionManager.SelectForGamepad(1);
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void MenuBack(InputAction.CallbackContext context)
    {
        _outGameActionManager.SelectForGamepad(-1);
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void MenuSelect(InputAction.CallbackContext context)
    {
        var key = context.control.name;
        if (key.Length > 1)
        {
            key = key.Substring(key.Length - 1);
        }
        _outGameActionManager.SelectForKeyboard(int.Parse(key) - 1);
    }

    /// <summary>
    /// エンターを押す関数
    /// </summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        _outGameActionManager.PushEnter();
    }

    /// <summary>
    /// キャンセルを押す関数
    /// </summary>
    /// <param name="context"></param>
    void PushCancel(InputAction.CallbackContext context)
    {
        _outGameActionManager.PushCansel();
    }

    /// <summary>
    /// 上方向に項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SelectUp(InputAction.CallbackContext context)
    {
        _outGameActionManager.VerticalArrowSelect(-1);
    }

    /// <summary>
    /// 下方向に項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SelectDown(InputAction.CallbackContext context)
    {
        _outGameActionManager.VerticalArrowSelect(1);
    }

    /// <summary>
    /// 右方向に項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SelectRight(InputAction.CallbackContext context)
    {
        _outGameActionManager.HorizontalArrowSelect(1);
    }

    /// <summary>
    /// 左方向に項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SelectLeft(InputAction.CallbackContext context)
    {
        _outGameActionManager.HorizontalArrowSelect(-1);
    }
}
