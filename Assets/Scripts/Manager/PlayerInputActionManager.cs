using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>プレイヤーの入力受付に関する制御を行うクラス</summary>
public class PlayerInputActionManager : InitializeBehaviour
{
    [SerializeField] InputActionAsset _actions;
    [SerializeField] Text _text;
    InputDevice _preDevice;
    InputActionMap _player, _ui;

    bool _isPlaying = false;
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
            _player = _actions.FindActionMap("Player");
            if (_player == null) FailedInitialization();
            _ui = _actions.FindActionMap("UI");
            if (_ui == null) FailedInitialization();
            ChangeActionMap();
        }

        //InputActionに割り当て
        //プレイ中
        _moveAct = InputSystem.actions.FindAction("Move");
        if (_moveAct == null) FailedInitialization();
        _downAct = InputSystem.actions.FindAction("Down");
        if (_downAct == null) FailedInitialization();
        _runAct = InputSystem.actions.FindAction("Run");
        if (_runAct == null) FailedInitialization();
        _jumpAct = InputSystem.actions.FindAction("Jump");
        if (_jumpAct == null) FailedInitialization();
        _interactAct = InputSystem.actions.FindAction("Interact");
        if (_interactAct == null) FailedInitialization();
        _itemAct = InputSystem.actions.FindAction("Item");
        if (_itemAct == null) FailedInitialization();
        _itemSlotAct = InputSystem.actions.FindActionMap("Player").FindAction("ItemSlot");
        if (_itemSlotAct == null)   FailedInitialization();
        _slotNextAct = InputSystem.actions.FindActionMap("Player").FindAction("SlotNext");
        if (_slotNextAct == null) FailedInitialization();
        _slotBackAct = InputSystem.actions.FindActionMap("Player").FindAction("SlotBack");
        if (_slotBackAct == null) FailedInitialization();
        _menuAct = InputSystem.actions.FindAction("Menu");
        if (_menuAct == null) FailedInitialization();

        //UI
        _menuNextAct = InputSystem.actions.FindAction("MenuNext");
        if (_menuNextAct == null) FailedInitialization();
        _menuBackAct = InputSystem.actions.FindAction("MenuBack");
        if (_menuBackAct == null) FailedInitialization();
        _menuSelectAct = InputSystem.actions.FindAction("MenuSelect");
        if (_menuSelectAct == null) FailedInitialization();
        _itemListAct = InputSystem.actions.FindAction("ItemList");
        if (_itemListAct == null) FailedInitialization();
        _itemSlotUIAct = InputSystem.actions.FindActionMap("UI").FindAction("ItemSlot");
        if (_itemSlotUIAct == null) FailedInitialization();
        _slotNextUIAct = InputSystem.actions.FindActionMap("UI").FindAction("SlotNext");
        if (_slotNextUIAct == null) FailedInitialization();
        _slotBackUIAct = InputSystem.actions.FindActionMap("UI").FindAction("SlotBack");
        if (_slotBackUIAct == null) FailedInitialization();
        _enterAct = InputSystem.actions.FindAction("Enter");
        if (_enterAct == null) FailedInitialization();
        _cancelAct = InputSystem.actions.FindAction("Cancel");
        if (_cancelAct == null) FailedInitialization();

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
        }
        return _isInitialized;
    }
    #endregion

    /// <summary>
    /// アクションマップを切り替える関数
    /// </summary>
    public void ChangeActionMap()
    {
        if (_isPlaying)
        {
            _ui.Enable();
            _player.Disable();
        }
        else
        {
            _player.Enable();
            _ui.Disable();
        }
        _isPlaying = !_isPlaying;
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
