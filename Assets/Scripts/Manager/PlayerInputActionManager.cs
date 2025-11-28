using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>プレイヤーの入力受付に関する制御を行うクラス</summary>
public class PlayerInputActionManager : InitializeBehaviour
{
    [SerializeField] InputActionAsset _actions;
    [SerializeField] string _startMapName;
    [SerializeField] Text _text;
    InputDevice _preDevice;
    InputActionMap _player, _ui,_outGame;

    const string PLAYER_MAP_NAME = "Player";
    const string UI_MAP_NAME = "UI";
    const string OUTGAME_MAP_NAME = "OutGame";
    public string PlayerMapName => PLAYER_MAP_NAME;
    public string UIMapName => UI_MAP_NAME;
    public string OutGameMapName => OUTGAME_MAP_NAME;
    #region InputAction
    //プレイ中
    InputAction _moveAct;
    InputAction _downAct;
    InputAction _runAct;
    InputAction _jumpAct;
    InputAction _interactAct;
    InputAction _itemAct;
    InputAction _itemSlotAct;
    InputAction _slotNextAct;
    InputAction _slotBackAct;
    InputAction _menuAct;
    //UI
    InputAction _menuNextAct;
    InputAction _menuBackAct;
    InputAction _menuSelectAct;
    InputAction _itemListAct;
    InputAction _itemSlotUIAct;
    InputAction _slotNextUIAct;
    InputAction _slotBackUIAct;
    InputAction _enterAct;
    InputAction _cancelAct;
    InputAction _selectUp;
    InputAction _selectDown;
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
    public InputAction MoveAct => _moveAct;
    public InputAction DownAct => _downAct;
    public InputAction RunAct => _runAct;
    public InputAction JumpAct => _jumpAct;
    public InputAction InteractAct => _interactAct;
    public InputAction ItemAct => _itemAct;
    public InputAction ItemSlotAct => _itemSlotAct;
    public InputAction SlotNextAct => _slotNextAct;
    public InputAction SlotBackAct => _slotBackAct;
    public InputAction MenuAct => _menuAct;
    //UI
    public InputAction MenuNextAct => _menuNextAct;
    public InputAction MenuBackAct => _menuBackAct;
    public InputAction MenuSelectAct => _menuSelectAct;
    public InputAction ItemListAct => _itemListAct;
    public InputAction ItemSlotUIAct => _itemSlotUIAct;
    public InputAction SlotNextUIAct => _slotNextUIAct;
    public InputAction SlotBackUIAct => _slotBackUIAct;
    public InputAction EnterAct => _enterAct;
    public InputAction CancelAct => _cancelAct;
    public InputAction SelectUp => _selectUp;
    public InputAction SelectDown => _selectDown;
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
            _player = _actions.FindActionMap(PLAYER_MAP_NAME);
            if (_player == null) FailedInitialization();
            _ui = _actions.FindActionMap(UI_MAP_NAME);
            if (_ui == null) FailedInitialization();
            _outGame = _actions.FindActionMap(OUTGAME_MAP_NAME);
            if (_outGame == null) FailedInitialization();
            ChangeActionMap(_startMapName);
        }

        //InputActionに割り当て
        //プレイ中
        _moveAct = _player.FindAction("Move");
        if (_moveAct == null) FailedInitialization();
        _downAct = _player.FindAction("Down");
        if (_downAct == null) FailedInitialization();
        _runAct = _player.FindAction("Run");
        if (_runAct == null) FailedInitialization();
        _jumpAct = _player.FindAction("Jump");
        if (_jumpAct == null) FailedInitialization();
        _interactAct = _player.FindAction("Interact");
        if (_interactAct == null) FailedInitialization();
        _itemAct = _player.FindAction("Item");
        if (_itemAct == null) FailedInitialization();
        _itemSlotAct = _player.FindAction("ItemSlot");
        if (_itemSlotAct == null) FailedInitialization();
        _slotNextAct = _player.FindAction("SlotNext");
        if (_slotNextAct == null) FailedInitialization();
        _slotBackAct = _player.FindAction("SlotBack");
        if (_slotBackAct == null) FailedInitialization();
        _menuAct = _player.FindAction("Menu");
        if (_menuAct == null) FailedInitialization();

        //UI
        _menuNextAct = _ui.FindAction("MenuNext");
        if (_menuNextAct == null) FailedInitialization();
        _menuBackAct = _ui.FindAction("MenuBack");
        if (_menuBackAct == null) FailedInitialization();
        _menuSelectAct = _ui.FindAction("MenuSelect");
        if (_menuSelectAct == null) FailedInitialization();
        _itemListAct = _ui.FindAction("ItemList");
        if (_itemListAct == null) FailedInitialization();
        _itemSlotUIAct = _ui.FindAction("ItemSlot");
        if (_itemSlotUIAct == null) FailedInitialization();
        _slotNextUIAct = _ui.FindAction("SlotNext");
        if (_slotNextUIAct == null) FailedInitialization();
        _slotBackUIAct = _ui.FindAction("SlotBack");
        if (_slotBackUIAct == null) FailedInitialization();
        _enterAct = _ui.FindAction("Enter");
        if (_enterAct == null) FailedInitialization();
        _cancelAct = _ui.FindAction("Cancel");
        if (_cancelAct == null) FailedInitialization();
        _selectUp = _ui.FindAction("SelectUp");
        if (_selectUp == null) FailedInitialization();
        _selectDown = _ui.FindAction("SelectDown");
        if (_selectDown == null) FailedInitialization();

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
            RegisterAct(_moveAct, GetCurrentControlDevice);
            RegisterAct(_downAct, GetCurrentControlDevice);
            RegisterAct(_runAct, GetCurrentControlDevice);
            RegisterAct(_jumpAct, GetCurrentControlDevice);
            RegisterAct(_interactAct, GetCurrentControlDevice);
            RegisterAct(_itemAct, GetCurrentControlDevice);
            RegisterAct(_itemSlotAct, GetCurrentControlDevice);
            RegisterAct(_slotNextAct, GetCurrentControlDevice);
            RegisterAct(_slotBackAct, GetCurrentControlDevice);
            RegisterAct(_menuAct, GetCurrentControlDevice);
            //UI
            RegisterAct(_menuNextAct, GetCurrentControlDevice);
            RegisterAct(_menuBackAct, GetCurrentControlDevice);
            RegisterAct(_menuSelectAct, GetCurrentControlDevice);
            RegisterAct(_itemListAct, GetCurrentControlDevice);
            RegisterAct(_itemSlotUIAct, GetCurrentControlDevice);
            RegisterAct(_slotNextUIAct, GetCurrentControlDevice);
            RegisterAct(_slotBackUIAct, GetCurrentControlDevice);
            RegisterAct(_enterAct, GetCurrentControlDevice);
            RegisterAct(_cancelAct, GetCurrentControlDevice);
            RegisterAct(_selectUp, GetCurrentControlDevice);
            RegisterAct(_selectDown, GetCurrentControlDevice);

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
    public void ChangeActionMap(string mapName)
    {
        switch (mapName)
        {
            case PLAYER_MAP_NAME:
                _outGame.Disable();
                _ui.Disable();
                _player.Enable();
                Debug.Log("CurrentMap is Player");
                break;
            case UI_MAP_NAME:
                _outGame.Disable();
                _player.Disable();
                _ui.Enable();
                Debug.Log("CurrentMap is UI");
                break;
            case OUTGAME_MAP_NAME:
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
