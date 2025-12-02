using ActionMap;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>プレイヤーの入力受付に関する制御を行うクラス</summary>
public class PlayerInputActionManager : InitializeBehaviour
{
    [SerializeField] InputActionAsset _actions;
    [SerializeField] ActionMapName _actionMapName = ActionMapName.Unknown;
    [SerializeField] Text _text;
    InputDevice _preDevice;
    InputActionMap _player, _ui, _outGame;
    Stack<ActionMapName> _actionMapStack;

    #region InputAction
    //プレイ中
    InputAction _moveActOnPlayScene;
    InputAction _downActOnPlayScene;
    InputAction _runActOnPlayScene;
    InputAction _jumpActOnPlayScene;
    InputAction _interactActOnPlayScene;
    InputAction _itemActOnPlayScene;
    InputAction _itemSlotActOnPlayScene;
    InputAction _slotNextActOnPlayScene;
    InputAction _slotBackActOnPlayScene;
    InputAction _menuActOnPlayScene;
    //UI
    InputAction _menuActOnUI;
    InputAction _menuSelectActOnUI;
    InputAction _itemListActOnUI;
    InputAction _slotNextActOnUI;
    InputAction _slotBackActOnUI;
    InputAction _enterActOnUI;
    InputAction _cancelActOnUI;
    InputAction _selectUpOnUI;
    InputAction _selectDownOnUI;
    //アウトゲーム
    InputAction _menuNextActOnOutGame;
    InputAction _menuBackActOnOutGame;
    InputAction _menuSelectActOnOutGame;
    InputAction _itemListActOnOutGame;
    InputAction _enterActOnOutGame;
    InputAction _cancelActOnOutGame;
    InputAction _selectUpOnOutGame;
    InputAction _selectDownOnOutGame;

    //プレイ中
    public InputAction MoveActOnPlayScene => _moveActOnPlayScene;
    public InputAction DownActOnPlayScene => _downActOnPlayScene;
    public InputAction RunActOnPlayScene => _runActOnPlayScene;
    public InputAction JumpActOnPlayScene => _jumpActOnPlayScene;
    public InputAction InteractActOnPlayScene => _interactActOnPlayScene;
    public InputAction ItemActOnPlayScene => _itemActOnPlayScene;
    public InputAction ItemSlotActOnPlayScene => _itemSlotActOnPlayScene;
    public InputAction SlotNextActOnPlayScene => _slotNextActOnPlayScene;
    public InputAction SlotBackActOnPlayScene => _slotBackActOnPlayScene;
    public InputAction MenuActOnPlayScene => _menuActOnPlayScene;
    //UI
    public InputAction MenuActOnUI => _menuActOnUI;
    public InputAction MenuSelectActOnUI => _menuSelectActOnUI;
    public InputAction ItemListActOnUI => _itemListActOnUI;
    public InputAction SlotNextActOnUI => _slotNextActOnUI;
    public InputAction SlotBackActOnUI => _slotBackActOnUI;
    public InputAction EnterActOnUI => _enterActOnUI;
    public InputAction CancelActOnUI => _cancelActOnUI;
    public InputAction SelectUpOnUI => _selectUpOnUI;
    public InputAction SelectDownOnUI => _selectDownOnUI;
    //アウトゲーム
    public InputAction MenuNextActOnOutGame => _menuNextActOnOutGame;
    public InputAction MenuBackActOnOutGame => _menuBackActOnOutGame;
    public InputAction MenuSelectActOnOutGame => _menuSelectActOnOutGame;
    public InputAction ItemListActOnOutGame => _itemListActOnOutGame;
    public InputAction EnterActOnOutGame => _enterActOnOutGame;
    public InputAction CancelActOnOutGame => _cancelActOnOutGame;
    public InputAction SelectUpOnOutGame => _selectUpOnOutGame;
    public InputAction SelectDownOnOutGame => _selectDownOnOutGame;
    #endregion

    #region 初期化
    public override bool Init(GameManager manager)
    {
        if (!_actions)
        {
            FailedInitialization();
        }
        else
        {
            _player = _actions.FindActionMap(ActionMapName.Player.ToString());
            if (_player == null) FailedInitialization();
            _ui = _actions.FindActionMap(ActionMapName.UI.ToString());
            if (_ui == null) FailedInitialization();
            _outGame = _actions.FindActionMap(ActionMapName.OutGame.ToString());
            if (_outGame == null) FailedInitialization();
            _actionMapStack = new Stack<ActionMapName>();
            ChangeActionMap(_actionMapName);
        }

        //InputActionに割り当て
        //プレイ中
        _moveActOnPlayScene = _player.FindAction("Move");
        if (_moveActOnPlayScene == null) FailedInitialization();
        _downActOnPlayScene = _player.FindAction("Down");
        if (_downActOnPlayScene == null) FailedInitialization();
        _runActOnPlayScene = _player.FindAction("Run");
        if (_runActOnPlayScene == null) FailedInitialization();
        _jumpActOnPlayScene = _player.FindAction("Jump");
        if (_jumpActOnPlayScene == null) FailedInitialization();
        _interactActOnPlayScene = _player.FindAction("Interact");
        if (_interactActOnPlayScene == null) FailedInitialization();
        _itemActOnPlayScene = _player.FindAction("Item");
        if (_itemActOnPlayScene == null) FailedInitialization();
        _itemSlotActOnPlayScene = _player.FindAction("ItemSlot");
        if (_itemSlotActOnPlayScene == null) FailedInitialization();
        _slotNextActOnPlayScene = _player.FindAction("SlotNext");
        if (_slotNextActOnPlayScene == null) FailedInitialization();
        _slotBackActOnPlayScene = _player.FindAction("SlotBack");
        if (_slotBackActOnPlayScene == null) FailedInitialization();
        _menuActOnPlayScene = _player.FindAction("Menu");
        if (_menuActOnPlayScene == null) FailedInitialization();

        //UI
        _menuActOnUI = _ui.FindAction("Menu");
        if (_menuActOnUI == null) FailedInitialization();
        _menuSelectActOnUI = _ui.FindAction("MenuSelect");
        if (_menuSelectActOnUI == null) FailedInitialization();
        _itemListActOnUI = _ui.FindAction("ItemList");
        if (_itemListActOnUI == null) FailedInitialization();
        _slotNextActOnUI = _ui.FindAction("SlotNext");
        if (_slotNextActOnUI == null) FailedInitialization();
        _slotBackActOnUI = _ui.FindAction("SlotBack");
        if (_slotBackActOnUI == null) FailedInitialization();
        _enterActOnUI = _ui.FindAction("Enter");
        if (_enterActOnUI == null) FailedInitialization();
        _cancelActOnUI = _ui.FindAction("Cancel");
        if (_cancelActOnUI == null) FailedInitialization();
        _selectUpOnUI = _ui.FindAction("SelectUp");
        if (_selectUpOnUI == null) FailedInitialization();
        _selectDownOnUI = _ui.FindAction("SelectDown");
        if (_selectDownOnUI == null) FailedInitialization();

        //アウトゲーム
        _menuNextActOnOutGame = _outGame.FindAction("MenuNext");
        if (_menuNextActOnOutGame == null) FailedInitialization();
        _menuBackActOnOutGame = _outGame.FindAction("MenuBack");
        if (_menuBackActOnOutGame == null) FailedInitialization();
        _menuSelectActOnOutGame = _outGame.FindAction("MenuSelect");
        if (_menuSelectActOnOutGame == null) FailedInitialization();
        _itemListActOnOutGame = _outGame.FindAction("ItemList");
        if (_itemListActOnOutGame == null) FailedInitialization();
        _enterActOnOutGame = _outGame.FindAction("Enter");
        if (_enterActOnOutGame == null) FailedInitialization();
        _cancelActOnOutGame = _outGame.FindAction("Cancel");
        if (_cancelActOnOutGame == null) FailedInitialization();
        _selectUpOnOutGame = _outGame.FindAction("SelectUp");
        if (_selectUpOnOutGame == null) FailedInitialization();
        _selectDownOnOutGame = _outGame.FindAction("SelectDown");
        if (_selectDownOnOutGame == null) FailedInitialization();

        if (!_isInitialized)
        {
            //プレイ中
            RegisterAct(_moveActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_downActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_runActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_jumpActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_interactActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_itemActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_itemSlotActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_slotNextActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_slotBackActOnPlayScene, GetCurrentControlDevice);
            RegisterAct(_menuActOnPlayScene, GetCurrentControlDevice);
            //UI
            RegisterAct(_menuActOnUI, GetCurrentControlDevice);
            RegisterAct(_menuSelectActOnUI, GetCurrentControlDevice);
            RegisterAct(_itemListActOnUI, GetCurrentControlDevice);
            RegisterAct(_slotNextActOnUI, GetCurrentControlDevice);
            RegisterAct(_slotBackActOnUI, GetCurrentControlDevice);
            RegisterAct(_enterActOnUI, GetCurrentControlDevice);
            RegisterAct(_cancelActOnUI, GetCurrentControlDevice);
            RegisterAct(_selectUpOnUI, GetCurrentControlDevice);
            RegisterAct(_selectDownOnUI, GetCurrentControlDevice);

            //アウトゲーム
            RegisterAct(_menuNextActOnOutGame, GetCurrentControlDevice);
            RegisterAct(_menuBackActOnOutGame, GetCurrentControlDevice);
            RegisterAct(_menuSelectActOnOutGame, GetCurrentControlDevice);
            RegisterAct(_itemListActOnOutGame, GetCurrentControlDevice);
            RegisterAct(_enterActOnOutGame, GetCurrentControlDevice);
            RegisterAct(_cancelActOnOutGame, GetCurrentControlDevice);
            RegisterAct(_selectUpOnOutGame, GetCurrentControlDevice);
            RegisterAct(_selectDownOnOutGame, GetCurrentControlDevice);
        }
        return _isInitialized;
    }
    #endregion
    /// <summary>
    /// アクションマップを切り替える関数
    /// </summary>
    /// <param name="mapName">切り替えるアクションマップの名前</param>
    public void ChangeActionMap(ActionMapName mapName = ActionMapName.Unknown)
    {
        if (mapName == ActionMapName.Unknown)
        {
            _actionMapStack.Pop();
        }
        else
        {
            _actionMapStack.Push(mapName);
        }

        switch (_actionMapStack.Peek())
        {
            case ActionMapName.Player:
                _outGame.Disable();
                _ui.Disable();
                _player.Enable();
                Debug.Log("CurrentMap is Player");
                break;
            case ActionMapName.UI:
                _outGame.Disable();
                _player.Disable();
                _ui.Enable();
                Debug.Log("CurrentMap is UI");
                break;
            case ActionMapName.OutGame:
                _player.Disable();
                _ui.Disable();
                _outGame.Enable();
                Debug.Log("CurrentMap is OutGame");
                break;
            default:
                Debug.LogError("No ActionMap Found");
                break;
        }
    }

    /// <summary>
    /// 最後に入力したデバイスに応じた処理をする関数
    /// </summary>
    /// <param name="context"></param>
    void GetCurrentControlDevice(InputAction.CallbackContext context)
    {
        var device = context.control.device;
        if (_preDevice != device)
        {
            _preDevice = device;
            Debug.Log($"Device Changed : {device}");
            if (device is Gamepad)
            {
                if (_text == null) return;
                _text.text = "Jump : South";
            }
            else if (device is Keyboard || device is UnityEngine.InputSystem.Mouse)
            {
                if (_text == null) return;
                _text.text = "Jump : Space";
            }
        }
    }

    /// <summary>
    /// InputActionに関数を登録する関数
    /// </summary>
    /// <param name="act">関数を登録するInputAction</param>
    /// <param name="context">登録する関数</param>
    public void RegisterAct(InputAction act, Action<InputAction.CallbackContext> context)
    {
        act.started += context;
    }

    /// <summary>
    /// InputActionから関数を解除する関数
    /// </summary>
    /// <param name="act">関数を解除するInputAction</param>
    /// <param name="context">解除する関数</param>
    public void UnregisterAct(InputAction act, Action<InputAction.CallbackContext> context)
    {
        act.started -= context;
    }
}

namespace ActionMap
{
    public enum ActionMapName
    {
        Player,
        UI,
        OutGame,
        Unknown
    }
}
