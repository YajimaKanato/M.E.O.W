using UnityEngine;
using UnityEngine.InputSystem;
using Interface;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] LayerMask _groundLayer;

    InputAction _moveAct, _jumpAct, _runAct, _interactAct, _itemAct;
    PlayerInput _playerInput;
    Rigidbody2D _rb2d;
    GameObject _target;
    IItemBaseEffective _item;

    RaycastHit2D _groundHit;
    Vector3 _move;
    Vector3 _rayStart, _rayEnd;

    #region 初期化など
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _jumpAct.started += Jump;
        _interactAct.started += EventAction;
        _itemAct.started += OpenItemList;
    }

    private void OnDisable()
    {
        _jumpAct.started -= Jump;
        _interactAct.started -= EventAction;
        _itemAct.started -= OpenItemList;
    }

    private void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChangeDetected;
    }

    // Update is called once per frame
    void Update()
    {
        _move = _moveAct.ReadValue<Vector2>() * _playerInfo.Status.Speed;

        _rayStart = transform.position + new Vector3(-0.5f, -0.6f);
        _rayEnd = transform.position + new Vector3(0.5f, -0.6f);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);
    }

    private void FixedUpdate()
    {
        Move(_runAct.IsPressed());
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {
        //InputActionに割り当て
        _moveAct = InputSystem.actions.FindAction("Move");
        _jumpAct = InputSystem.actions.FindAction("Jump");
        _runAct = InputSystem.actions.FindAction("Run");
        _interactAct = InputSystem.actions.FindAction("Interact");
        _itemAct = InputSystem.actions.FindAction("Item");
        _rb2d = GetComponent<Rigidbody2D>();

        //_playerInput = GetComponent<PlayerInput>();
        //_playerInput.neverAutoSwitchControlSchemes = true;
        InputSystem.onDeviceChange += OnDeviceChangeDetected;
        UpdateDeviceBinding();
    }
    #endregion

    #region InputSystem関連
    /// <summary>
    /// 入力デバイスに対してコントロール権を変更する関数
    /// </summary>
    /// <param name="device"></param>
    /// <param name="change"></param>
    void OnDeviceChangeDetected(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            UpdateDeviceBinding();
        }
    }

    /// <summary>
    /// コントロール権の対応をする関数
    /// </summary>
    void UpdateDeviceBinding()
    {
        //どちらか固定
        //if (Gamepad.all.Count > 0)
        //{
        //    _playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
        //}
        //else
        //{
        //    _playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", new InputDevice[] { Keyboard.current, UnityEngine.InputSystem.Mouse.current });
        //}

        //入力に応じて切り替わる
        var actions = InputSystem.actions;
        actions.bindingMask = InputBinding.MaskByGroups(Gamepad.all.Count > 0 ? "Gamepad" : "Keyboard&Mouse");
    }

    /// <summary>
    /// 移動する関数
    /// </summary>
    /// <param name="isRun">ダッシュかどうか</param>
    void Move(bool isRun)
    {
        //ダッシュか否か
        if (isRun)
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _playerInfo.Status.MaxRunSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
        else
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _playerInfo.Status.MaxWalkSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
    }

    /// <summary>
    /// ジャンプする関数
    /// </summary>
    /// <param name="context"></param>
    void Jump(InputAction.CallbackContext context)
    {
        if (_groundHit) _rb2d.AddForce(Vector3.up * _playerInfo.Status.Jump, ForceMode2D.Impulse);
    }

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <param name="context"></param>
    void EventAction(InputAction.CallbackContext context)
    {
        if (_target)
        {
            Interact();
        }
        else
        {
            Debug.Log("Not Event");
        }
    }

    /// <summary>
    /// アイテムリストを開く関数
    /// </summary>
    /// <param name="context"></param>
    void OpenItemList(InputAction.CallbackContext context)
    {
        ButtonActions.ChangeScene("Bag");
        Debug.Log("Open ItemList");
    }
    #endregion

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    void ItemSelect()
    {
        _item = _playerInfo.ItemSlot.SelectItem(0);
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    void ItemUse()
    {
        GameEventManager.ItemUse(_item, _playerInfo);
    }

    /// <summary>
    /// インタラクトをする関数
    /// </summary>
    void Interact()
    {
        GameEventManager.Interact(_target.GetComponent<EventBase>(), _playerInfo);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Event")
        {
            if (!_target)
            {
                _target = collision.gameObject;
            }
            else
            {
                //一番近いキャラクターをターゲットとする
                if (Vector3.Distance(_target.transform.position, transform.position) > Vector3.Distance(collision.gameObject.transform.position, transform.position))
                {
                    _target = collision.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_target)
        {
            _target = null;
        }
    }
}
