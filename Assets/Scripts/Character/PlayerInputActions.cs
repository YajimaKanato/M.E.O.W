using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputActions : MonoBehaviour
{
    [SerializeField] Text _text;
    InputAction _moveAct, _downAct, _jumpAct, _runAct, _interactAct, _itemAct, _enterAct;
    InputDevice _preDevice;
    static PlayerInputActions _instance;
    public InputAction MoveAct => _moveAct;
    public InputAction DownAct => _downAct;
    public InputAction JumpAct => _jumpAct;
    public InputAction RunAct => _runAct;
    public InputAction InteractAct => _interactAct;
    public InputAction ItemAct => _itemAct;
    public InputAction EnterAct => _enterAct;

    private void Awake()
    {
        if( _instance == null)
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
        RegisterAct(_moveAct, GetCurrentControlDevice);
        RegisterAct(_downAct, GetCurrentControlDevice);
        RegisterAct(_jumpAct, GetCurrentControlDevice);
        RegisterAct(_runAct, GetCurrentControlDevice);
        RegisterAct(_interactAct, GetCurrentControlDevice);
        RegisterAct(_itemAct, GetCurrentControlDevice);
        RegisterAct(_enterAct, GetCurrentControlDevice);
    }

    private void OnDisable()
    {
        UnregisterAct(_moveAct, GetCurrentControlDevice);
        UnregisterAct(_downAct, GetCurrentControlDevice);
        UnregisterAct(_jumpAct, GetCurrentControlDevice);
        UnregisterAct(_runAct, GetCurrentControlDevice);
        UnregisterAct(_interactAct, GetCurrentControlDevice);
        UnregisterAct(_itemAct, GetCurrentControlDevice);
        UnregisterAct(_enterAct, GetCurrentControlDevice);
    }

    void Init()
    {
        //InputAction‚ÉŠ„‚è“–‚Ä
        _moveAct = InputSystem.actions.FindAction("Move");
        _downAct = InputSystem.actions.FindAction("Down");
        _jumpAct = InputSystem.actions.FindAction("Jump");
        _runAct = InputSystem.actions.FindAction("Run");
        _interactAct = InputSystem.actions.FindAction("Interact");
        _itemAct = InputSystem.actions.FindAction("Item");
        _enterAct = InputSystem.actions.FindAction("Enter");
    }

    /// <summary>
    /// ÅŒã‚É“ü—Í‚µ‚½ƒfƒoƒCƒX‚É‰‚¶‚½ˆ—‚ğ‚·‚éŠÖ”
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
                _text.text = "Jump : SOUTH";
            }
            else if (device is Keyboard || device is UnityEngine.InputSystem.Mouse)
            {
                _text.text = "Jump : Space";
            }
        }
    }

    /// <summary>
    /// InputAction‚ÉŠÖ”‚ğ“o˜^‚·‚éŠÖ”
    /// </summary>
    /// <param name="act">ŠÖ”‚ğ“o˜^‚·‚éInputAction</param>
    /// <param name="context">“o˜^‚·‚éŠÖ”</param>
    public void RegisterAct(InputAction act, Action<InputAction.CallbackContext> context)
    {
        act.started += context;
    }

    /// <summary>
    /// InputAction‚©‚çŠÖ”‚ğ‰ğœ‚·‚éŠÖ”
    /// </summary>
    /// <param name="act">ŠÖ”‚ğ‰ğœ‚·‚éInputAction</param>
    /// <param name="context">‰ğœ‚·‚éŠÖ”</param>
    public void UnregisterAct(InputAction act, Action<InputAction.CallbackContext> context)
    {
        act.started -= context;
    }
}
