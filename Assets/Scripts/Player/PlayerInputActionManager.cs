using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputActionManager : MonoBehaviour
{
    [SerializeField] Text _text;
    InputDevice _preDevice;
    static PlayerInputActionManager _instance;
    public static PlayerInputActionManager Instance => _instance;
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

    #region Unityメッセージなど
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
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

    private void OnDisable()
    {
        //プレイ中
        UnregisterAct(_moveAct, GetCurrentControlDevice);
        UnregisterAct(_downAct, GetCurrentControlDevice);
        UnregisterAct(_runAct, GetCurrentControlDevice);
        UnregisterAct(_jumpAct, GetCurrentControlDevice);
        UnregisterAct(_interactAct, GetCurrentControlDevice);
        UnregisterAct(_itemAct, GetCurrentControlDevice);
        UnregisterAct(_itemSlotAct, GetCurrentControlDevice);
        UnregisterAct(_slotNextAct, GetCurrentControlDevice);
        UnregisterAct(_slotBackAct, GetCurrentControlDevice);
        UnregisterAct(_menuAct, GetCurrentControlDevice);
        //UI
        UnregisterAct(_menuNextAct, GetCurrentControlDevice);
        UnregisterAct(_menuBackAct, GetCurrentControlDevice);
        UnregisterAct(_menuSelectAct, GetCurrentControlDevice);
        UnregisterAct(_itemListAct, GetCurrentControlDevice);
        UnregisterAct(_itemSlotUIAct, GetCurrentControlDevice);
        UnregisterAct(_slotNextUIAct, GetCurrentControlDevice);
        UnregisterAct(_slotBackUIAct, GetCurrentControlDevice);
        UnregisterAct(_enterAct, GetCurrentControlDevice);
        UnregisterAct(_cancelAct, GetCurrentControlDevice);
    }

    void Init()
    {
        //InputActionに割り当て
        //プレイ中
        _moveAct = InputSystem.actions.FindAction("Move");
        _downAct = InputSystem.actions.FindAction("Down");
        _runAct = InputSystem.actions.FindAction("Run");
        _jumpAct = InputSystem.actions.FindAction("Jump");
        _interactAct = InputSystem.actions.FindAction("Interact");
        _itemAct = InputSystem.actions.FindAction("Item");
        _itemSlotAct = InputSystem.actions.FindAction("Player/ItemSlot");
        _slotNextAct = InputSystem.actions.FindAction("Player/SlotNext");
        _slotBackAct = InputSystem.actions.FindAction("Player/SlotBack");
        _menuAct = InputSystem.actions.FindAction("Menu");
        //UI
        _menuNextAct = InputSystem.actions.FindAction("MenuNext");
        _menuBackAct = InputSystem.actions.FindAction("MenuBack");
        _menuSelectAct = InputSystem.actions.FindAction("MenuSelect");
        _itemListAct = InputSystem.actions.FindAction("ItemList");
        _itemSlotUIAct = InputSystem.actions.FindAction("UI/ItemSlot");
        _slotNextUIAct = InputSystem.actions.FindAction("UI/SlotNext");
        _slotBackUIAct = InputSystem.actions.FindAction("UI/SlotBack");
        _enterAct = InputSystem.actions.FindAction("Enter");
        _cancelAct = InputSystem.actions.FindAction("Cancel");
    }
    #endregion

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
                _text.text = "Jump : South";
            }
            else if (device is Keyboard || device is UnityEngine.InputSystem.Mouse)
            {
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
