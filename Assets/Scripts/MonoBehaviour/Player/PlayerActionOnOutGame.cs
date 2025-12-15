using UnityEngine.InputSystem;

public class PlayerActionOnOutGame : InitializeBehaviour
{
    PlayerInputActionManager _playerInputActionManager;
    OutGameActionManager _outGameActionManager;
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _playerInputActionManager, _gameManager.PlayerInputActionManager);
        InitializeManager.InitializationForVariable(out _outGameActionManager, _gameManager.OutGameActionManager);
        if (_isInitialized)
        {
            if (_playerInputActionManager == null)
            {
                InitializeManager.FailedInitialization();
            }
            else if (_outGameActionManager == null)
            {
                InitializeManager.FailedInitialization();
            }
            else
            {
                _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuNextActOnOutGame, MenuNext);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuBackActOnOutGame, MenuBack);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuSelectActOnOutGame, MenuSelect);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.EnterActOnOutGame, PushEnter);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.CancelActOnOutGame, PushCancel);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectUpOnOutGame, SelectUp);
                _playerInputActionManager.RegisterAct(_playerInputActionManager.SelectDownOnOutGame, SelectDown);
            }
        }
        return _isInitialized;
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void MenuNext(InputAction.CallbackContext context)
    {
        _outGameActionManager.MenuSelectForGamepad(1);
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void MenuBack(InputAction.CallbackContext context)
    {
        _outGameActionManager.MenuSelectForGamepad(-1);
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
        _outGameActionManager.MenuSelectForKeyboard(int.Parse(key) - 1);
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
    /// タイトルの項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SelectUp(InputAction.CallbackContext context)
    {
        _outGameActionManager.TitleSelect(-1);
    }

    /// <summary>
    /// タイトルの項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SelectDown(InputAction.CallbackContext context)
    {
        _outGameActionManager.TitleSelect(1);
    }
}
