using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] PlayerStatus _status;
    [SerializeField] LayerMask _groundLayer;

    InputAction _moveAct, _jumpAct, _runAct, _interactAct, _itemAct;
    PlayerInput _playerInput;
    Rigidbody2D _rb2d;
    GameObject _target;

    RaycastHit2D _groundHit;
    Vector3 _move;
    Vector3 _rayStart, _rayEnd;

    float _currentHP;
    float _currentFullness;

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
        _move = _moveAct.ReadValue<Vector2>() * _status.Speed;

        _rayStart = transform.position + new Vector3(-0.5f, -0.6f);
        _rayEnd = transform.position + new Vector3(0.5f, -0.6f);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);
    }

    private void FixedUpdate()
    {
        //ダッシュか否か
        if (_runAct.IsPressed())
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _status.MaxRunSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
        else
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _status.MaxWalkSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
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

        //初期ステータス
        _currentHP = _status.HP;
        _currentFullness = _status.Fullness;
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
    /// ジャンプする関数
    /// </summary>
    /// <param name="context"></param>
    void Jump(InputAction.CallbackContext context)
    {
        if (_groundHit) _rb2d.AddForce(Vector3.up * _status.Jump, ForceMode2D.Impulse);
    }

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <param name="context"></param>
    void EventAction(InputAction.CallbackContext context)
    {
        if (_target)
        {
            _target.GetComponent<EventBase>().Event();
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
    /// 満腹度を回復する関数
    /// </summary>
    /// <param name="fullness">回復量</param>
    public void Saturation(float fullness)
    {
        _currentFullness += fullness;
        if (_currentFullness >= _status.Fullness) _currentFullness = _status.Fullness;
    }

    /// <summary>
    /// 必要に応じてHPを更新する関数
    /// </summary>
    /// <param name="value">変化量</param>
    public void HPUpdate(float value)
    {
        _currentHP += value;
        if (_currentHP >= _status.HP) _currentHP = _status.HP;
        if (_currentHP <= 0) _currentHP = 0;
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    void ItemUse()
    {

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
