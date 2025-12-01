using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionOnOutGame : InitializeBehaviour
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
            if (_gameManager.PlayerInputActionManager == null)
            {
                FailedInitialization();
            }
            else if (_gameManager.DataManager == null)
            {
                FailedInitialization();
            }
            else if (_gameManager.OutGameActionManager == null)
            {
                FailedInitialization();
            }
            else
            {
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.MenuNextActOnOutGame, MenuNext);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.MenuBackActOnOutGame, MenuBack);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.MenuSelectActOnOutGame, MenuSelect);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.ItemListActOnOutGame, ItemList);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.EnterActOnOutGame, PushEnter);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.CancelActOnOutGame, PushCancel);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SelectUpOnOutGame, SelectUp);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SelectDownOnOutGame, SelectDown);
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
        _gameManager.OutGameActionManager.MenuSelectForGamepad(1);
    }

    /// <summary>
    /// メニュー項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void MenuBack(InputAction.CallbackContext context)
    {
        _gameManager.OutGameActionManager.MenuSelectForGamepad(-1);
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
        _gameManager.OutGameActionManager.MenuSelectForKeyboard(int.Parse(key) - 1);
    }

    /// <summary>
    /// アイテムリストを操作する関数
    /// </summary>
    /// <param name="context"></param>
    void ItemList(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// エンターを押す関数
    /// </summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        _gameManager.OutGameActionManager.PushEnter();
    }

    /// <summary>
    /// キャンセルを押す関数
    /// </summary>
    /// <param name="context"></param>
    void PushCancel(InputAction.CallbackContext context)
    {
        _gameManager.OutGameActionManager.PushCansel();
    }

    /// <summary>
    /// タイトルの項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SelectUp(InputAction.CallbackContext context)
    {
        _gameManager.OutGameActionManager.TitleSelect(-1);
    }

    /// <summary>
    /// タイトルの項目を選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SelectDown(InputAction.CallbackContext context)
    {
        _gameManager.OutGameActionManager.TitleSelect(1);
    }
}
