using ActionMap;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>プレイヤーの入力受付に関する制御を行うクラス</summary>
public class PlayerInputActionManager : InitializeBehaviour
{
    [SerializeField] InputActionAsset _actions;
    [SerializeField] ActionMapName _actionMapName = ActionMapName.Unknown;
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
            InitializationForVariable(out _player, _actions.FindActionMap(ActionMapName.Player.ToString()));
            InitializationForVariable(out _ui, _actions.FindActionMap(ActionMapName.UI.ToString()));
            InitializationForVariable(out _outGame, _actions.FindActionMap(ActionMapName.OutGame.ToString()));
            InitializationForVariable(out _actionMapStack, new Stack<ActionMapName>());
            ChangeActionMap(_actionMapName);
        }

        //InputActionに割り当て
        //プレイ中
        InitializationForVariable(out _moveActOnPlayScene, _player.FindAction("Move"));
        InitializationForVariable(out _downActOnPlayScene, _player.FindAction("Down"));
        InitializationForVariable(out _runActOnPlayScene, _player.FindAction("Run"));
        InitializationForVariable(out _jumpActOnPlayScene, _player.FindAction("Jump"));
        InitializationForVariable(out _interactActOnPlayScene, _player.FindAction("Interact"));
        InitializationForVariable(out _itemActOnPlayScene, _player.FindAction("Item"));
        InitializationForVariable(out _itemSlotActOnPlayScene, _player.FindAction("ItemSlot"));
        InitializationForVariable(out _slotNextActOnPlayScene, _player.FindAction("SlotNext"));
        InitializationForVariable(out _slotBackActOnPlayScene, _player.FindAction("SlotBack"));
        InitializationForVariable(out _menuActOnPlayScene, _player.FindAction("Menu"));

        //UI
        InitializationForVariable(out _menuActOnUI, _ui.FindAction("Menu"));
        InitializationForVariable(out _menuSelectActOnUI, _ui.FindAction("MenuSelect"));
        InitializationForVariable(out _itemListActOnUI, _ui.FindAction("ItemList"));
        InitializationForVariable(out _slotNextActOnUI, _ui.FindAction("SlotNext"));
        InitializationForVariable(out _slotBackActOnUI, _ui.FindAction("SlotBack"));
        InitializationForVariable(out _enterActOnUI, _ui.FindAction("Enter"));
        InitializationForVariable(out _cancelActOnUI, _ui.FindAction("Cancel"));
        InitializationForVariable(out _selectUpOnUI, _ui.FindAction("SelectUp"));
        InitializationForVariable(out _selectDownOnUI, _ui.FindAction("SelectDown"));

        //アウトゲーム
        InitializationForVariable(out _menuNextActOnOutGame, _outGame.FindAction("MenuNext"));
        InitializationForVariable(out _menuBackActOnOutGame, _outGame.FindAction("MenuBack"));
        InitializationForVariable(out _menuSelectActOnOutGame, _outGame.FindAction("MenuSelect"));
        InitializationForVariable(out _itemListActOnOutGame, _outGame.FindAction("ItemList"));
        InitializationForVariable(out _enterActOnOutGame, _outGame.FindAction("Enter"));
        InitializationForVariable(out _cancelActOnOutGame, _outGame.FindAction("Cancel"));
        InitializationForVariable(out _selectUpOnOutGame, _outGame.FindAction("SelectUp"));
        InitializationForVariable(out _selectDownOnOutGame, _outGame.FindAction("SelectDown"));

        if (_isInitialized)
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

            ////プレイ中
            //RegisterAct(_moveActOnPlayScene, _ => Debug.Log($"{_moveActOnPlayScene}"));
            //RegisterAct(_downActOnPlayScene, _ => Debug.Log($"{_downActOnPlayScene}"));
            //RegisterAct(_runActOnPlayScene, _ => Debug.Log($"{_runActOnPlayScene}"));
            //RegisterAct(_jumpActOnPlayScene, _ => Debug.Log($"{_jumpActOnPlayScene}"));
            //RegisterAct(_interactActOnPlayScene, _ => Debug.Log($"{_interactActOnPlayScene}"));
            //RegisterAct(_itemActOnPlayScene, _ => Debug.Log($"{_itemActOnPlayScene}"));
            //RegisterAct(_itemSlotActOnPlayScene, _ => Debug.Log($"{_itemSlotActOnPlayScene}"));
            //RegisterAct(_slotNextActOnPlayScene, _ => Debug.Log($"{_slotNextActOnPlayScene}"));
            //RegisterAct(_slotBackActOnPlayScene, _ => Debug.Log($"{_slotBackActOnPlayScene}"));
            //RegisterAct(_menuActOnPlayScene, _ => Debug.Log($"{_menuActOnPlayScene}"));
            ////UI
            //RegisterAct(_menuActOnUI, _ => Debug.Log($"{_menuActOnUI}"));
            //RegisterAct(_menuSelectActOnUI, _ => Debug.Log($"{_menuSelectActOnUI}"));
            //RegisterAct(_itemListActOnUI, _ => Debug.Log($"{_itemListActOnUI}"));
            //RegisterAct(_slotNextActOnUI, _ => Debug.Log($"{_slotNextActOnUI}"));
            //RegisterAct(_slotBackActOnUI, _ => Debug.Log($"{_slotBackActOnUI}"));
            //RegisterAct(_enterActOnUI, _ => Debug.Log($"{_enterActOnUI}"));
            //RegisterAct(_cancelActOnUI, _ => Debug.Log($"{_cancelActOnUI}"));
            //RegisterAct(_selectUpOnUI, _ => Debug.Log($"{_selectUpOnUI}"));
            //RegisterAct(_selectDownOnUI, _ => Debug.Log($"{_selectDownOnUI}"));

            ////アウトゲーム
            //RegisterAct(_menuNextActOnOutGame, _ => Debug.Log($"{_menuNextActOnOutGame}"));
            //RegisterAct(_menuBackActOnOutGame, _ => Debug.Log($"{_menuBackActOnOutGame}"));
            //RegisterAct(_menuSelectActOnOutGame, _ => Debug.Log($"{_menuSelectActOnOutGame}"));
            //RegisterAct(_itemListActOnOutGame, _ => Debug.Log($"{_itemListActOnOutGame}"));
            //RegisterAct(_enterActOnOutGame, _ => Debug.Log($"{_enterActOnOutGame}"));
            //RegisterAct(_cancelActOnOutGame, _ => Debug.Log($"{_cancelActOnOutGame}"));
            //RegisterAct(_selectUpOnOutGame, _ => Debug.Log($"{_selectUpOnOutGame}"));
            //RegisterAct(_selectDownOnOutGame, _ => Debug.Log($"{_selectDownOnOutGame}"));
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
            }
            else if (device is Keyboard || device is UnityEngine.InputSystem.Mouse)
            {
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
