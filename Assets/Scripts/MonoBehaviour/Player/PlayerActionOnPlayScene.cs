using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerActionOnPlayScene : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] LayerMask _groundLayer;
    Rigidbody2D _rb2d;
    GameObject _target;
    GameManager _initManager;

    RaycastHit2D _groundHit;
    Vector3 _move;
    Vector3 _rayStart, _rayEnd;

    #region Unityメッセージなど
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _initManager.PlayerInputActionManager.RegisterAct(_initManager.PlayerInputActionManager.DownAct, Down);
        _initManager.PlayerInputActionManager.RegisterAct(_initManager.PlayerInputActionManager.JumpAct, Jump);
        _initManager.PlayerInputActionManager.RegisterAct(_initManager.PlayerInputActionManager.InteractAct, EventAction);
        _initManager.PlayerInputActionManager.RegisterAct(_initManager.PlayerInputActionManager.ItemAct, ItemUse);
        _initManager.PlayerInputActionManager.RegisterAct(_initManager.PlayerInputActionManager.EnterAct, PushEnter);
        _initManager.PlayerInputActionManager.RegisterAct(_initManager.PlayerInputActionManager.ItemSlotAct, ItemSelectForKeyboard);
        _initManager.PlayerInputActionManager.RegisterAct(_initManager.PlayerInputActionManager.SlotNextAct, SlotNextForGamepad);
        _initManager.PlayerInputActionManager.RegisterAct(_initManager.PlayerInputActionManager.SlotBackAct, SlotBackForGamepad);
    }

    private void OnDisable()
    {
        _initManager.PlayerInputActionManager.UnregisterAct(_initManager.PlayerInputActionManager.DownAct, Down);
        _initManager.PlayerInputActionManager.UnregisterAct(_initManager.PlayerInputActionManager.JumpAct, Jump);
        _initManager.PlayerInputActionManager.UnregisterAct(_initManager.PlayerInputActionManager.InteractAct, EventAction);
        _initManager.PlayerInputActionManager.UnregisterAct(_initManager.PlayerInputActionManager.ItemAct, ItemUse);
        _initManager.PlayerInputActionManager.UnregisterAct(_initManager.PlayerInputActionManager.EnterAct, PushEnter);
        _initManager.PlayerInputActionManager.UnregisterAct(_initManager.PlayerInputActionManager.ItemSlotAct, ItemSelectForKeyboard);
        _initManager.PlayerInputActionManager.UnregisterAct(_initManager.PlayerInputActionManager.SlotNextAct, SlotNextForGamepad);
        _initManager.PlayerInputActionManager.UnregisterAct(_initManager.PlayerInputActionManager.SlotBackAct, SlotBackForGamepad);
    }

    // Update is called once per frame
    void Update()
    {
        //移動に関する処理
        _move = _initManager.PlayerInputActionManager.MoveAct.ReadValue<Vector2>() * _playerInfo.Speed;

        //接地判定を取る処理
        _rayStart = transform.position + new Vector3(-0.5f, -0.6f);
        _rayEnd = transform.position + new Vector3(0.5f, -0.6f);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);

        //インタラクト対象を取得する処理
        _initManager.GameActionManager.GetTarget(transform);
    }

    private void FixedUpdate()
    {
        Move(_initManager.PlayerInputActionManager.RunAct.IsPressed());
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _initManager = GameManager.Instance;
    }
    #endregion

    #region InputSystem関連
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
            if (Mathf.Abs(_rb2d.linearVelocityX) < _playerInfo.MaxRunSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
        else
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _playerInfo.MaxWalkSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
    }

    /// <summary>
    /// 足場から降りる関数
    /// </summary>
    /// <param name="context"></param>
    void Down(InputAction.CallbackContext context)
    {
        Debug.Log("Down");
    }

    /// <summary>
    /// ジャンプする関数
    /// </summary>
    /// <param name="context"></param>
    void Jump(InputAction.CallbackContext context)
    {
        if (_groundHit) _rb2d.AddForce(Vector3.up * _playerInfo.Jump, ForceMode2D.Impulse);
    }

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <param name="context"></param>
    void EventAction(InputAction.CallbackContext context)
    {
        _initManager.GameActionManager.Interact(_playerInfo);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void ItemSelectForKeyboard(InputAction.CallbackContext context)
    {
        var key = context.control.name;
        if (key.Length > 1)
        {
            key = key.Substring(key.Length - 1);
        }
        Debug.Log(key);
        _initManager.GameActionManager.ItemSelectForKeyboard(int.Parse(key) - 1, _playerInfo);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotNextForGamepad(InputAction.CallbackContext context)
    {
        _initManager.GameActionManager.ItemSelectForGamepad(1, _playerInfo);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotBackForGamepad(InputAction.CallbackContext context)
    {
        _initManager.GameActionManager.ItemSelectForGamepad(-1, _playerInfo);
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    void ItemUse(InputAction.CallbackContext context)
    {
        _initManager.GameActionManager.ItemUse(_playerInfo);
    }

    /// <summary>
    /// エンターを押したときに行う関数
    /// </summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        _initManager.GameActionManager.PushEnterUntilTalking();
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            _initManager.GameActionManager.AddTargetList(collision.gameObject.GetComponent<EventBase>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            _initManager.GameActionManager.RemoveTargetList(collision.gameObject.GetComponent<EventBase>());
        }
    }
}
