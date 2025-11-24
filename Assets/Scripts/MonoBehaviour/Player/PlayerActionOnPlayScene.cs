using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerActionOnPlayScene : InitializeBehaviour
{
    [SerializeField] LayerMask _groundLayer;
    Rigidbody2D _rb2d;

    RaycastHit2D _groundHit;
    Vector3 _move;
    Vector3 _rayStart, _rayEnd;

    #region Unityメッセージなど
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //Init();
    }
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) return false;

        if(TryGetComponent<Rigidbody2D>(out var rb2d))
        {
            _rb2d = rb2d;
        }
        else
        {
            return false;
        }

        _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.DownAct, Down);
        _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.JumpAct, Jump);
        _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.InteractAct, EventAction);
        _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.ItemAct, ItemUse);
        _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.EnterAct, PushEnter);
        _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.ItemSlotAct, ItemSelectForKeyboard);
        _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SlotNextAct, SlotNextForGamepad);
        _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SlotBackAct, SlotBackForGamepad);
        return true;
    }

    //private void OnEnable()
    //{
    //    _playerInfo.InitManager.PlayerInputActionManager.RegisterAct(_playerInfo.InitManager.PlayerInputActionManager.DownAct, Down);
    //    _playerInfo.InitManager.PlayerInputActionManager.RegisterAct(_playerInfo.InitManager.PlayerInputActionManager.JumpAct, Jump);
    //    _playerInfo.InitManager.PlayerInputActionManager.RegisterAct(_playerInfo.InitManager.PlayerInputActionManager.InteractAct, EventAction);
    //    _playerInfo.InitManager.PlayerInputActionManager.RegisterAct(_playerInfo.InitManager.PlayerInputActionManager.ItemAct, ItemUse);
    //    _playerInfo.InitManager.PlayerInputActionManager.RegisterAct(_playerInfo.InitManager.PlayerInputActionManager.EnterAct, PushEnter);
    //    _playerInfo.InitManager.PlayerInputActionManager.RegisterAct(_playerInfo.InitManager.PlayerInputActionManager.ItemSlotAct, ItemSelectForKeyboard);
    //    _playerInfo.InitManager.PlayerInputActionManager.RegisterAct(_playerInfo.InitManager.PlayerInputActionManager.SlotNextAct, SlotNextForGamepad);
    //    _playerInfo.InitManager.PlayerInputActionManager.RegisterAct(_playerInfo.InitManager.PlayerInputActionManager.SlotBackAct, SlotBackForGamepad);
    //}

    //private void OnDisable()
    //{
    //    _playerInfo.InitManager.PlayerInputActionManager.UnregisterAct(_playerInfo.InitManager.PlayerInputActionManager.DownAct, Down);
    //    _playerInfo.InitManager.PlayerInputActionManager.UnregisterAct(_playerInfo.InitManager.PlayerInputActionManager.JumpAct, Jump);
    //    _playerInfo.InitManager.PlayerInputActionManager.UnregisterAct(_playerInfo.InitManager.PlayerInputActionManager.InteractAct, EventAction);
    //    _playerInfo.InitManager.PlayerInputActionManager.UnregisterAct(_playerInfo.InitManager.PlayerInputActionManager.ItemAct, ItemUse);
    //    _playerInfo.InitManager.PlayerInputActionManager.UnregisterAct(_playerInfo.InitManager.PlayerInputActionManager.EnterAct, PushEnter);
    //    _playerInfo.InitManager.PlayerInputActionManager.UnregisterAct(_playerInfo.InitManager.PlayerInputActionManager.ItemSlotAct, ItemSelectForKeyboard);
    //    _playerInfo.InitManager.PlayerInputActionManager.UnregisterAct(_playerInfo.InitManager.PlayerInputActionManager.SlotNextAct, SlotNextForGamepad);
    //    _playerInfo.InitManager.PlayerInputActionManager.UnregisterAct(_playerInfo.InitManager.PlayerInputActionManager.SlotBackAct, SlotBackForGamepad);
    //}

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager) return;
        //移動に関する処理
        _move = _gameManager.PlayerInputActionManager.MoveAct.ReadValue<Vector2>() * _gameManager.StatusManager.PlayerRunTime.Speed;

        //接地判定を取る処理
        _rayStart = transform.position + new Vector3(-0.5f, -0.6f);
        _rayEnd = transform.position + new Vector3(0.5f, -0.6f);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);

        //インタラクト対象を取得する処理
        _gameManager.GameActionManager.GetTarget(transform);
    }

    private void FixedUpdate()
    {
        if (!_gameManager) return;
        Move(_gameManager.PlayerInputActionManager.RunAct.IsPressed());
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {
        _rb2d = GetComponent<Rigidbody2D>();
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
            if (Mathf.Abs(_rb2d.linearVelocityX) < _gameManager.StatusManager.PlayerRunTime.MaxRunSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
        else
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _gameManager.StatusManager.PlayerRunTime.MaxWalkSpeed)
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
        if (_groundHit) _rb2d.AddForce(Vector3.up * _gameManager.StatusManager.PlayerRunTime.Jump, ForceMode2D.Impulse);
    }

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <param name="context"></param>
    void EventAction(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.Interact();
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
        _gameManager.GameActionManager.ItemSelectForKeyboard(int.Parse(key) - 1);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotNextForGamepad(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.ItemSelectForGamepad(1);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotBackForGamepad(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.ItemSelectForGamepad(-1);
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    void ItemUse(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.ItemUse();
    }

    /// <summary>
    /// エンターを押したときに行う関数
    /// </summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.PushEnterUntilTalking();
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            if (collision.gameObject.TryGetComponent<CharacterNPC>(out var character))
            {
                _gameManager.GameActionManager.AddTargetList(character);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            if (collision.gameObject.TryGetComponent<CharacterNPC>(out var character))
            {
                _gameManager.GameActionManager.RemoveTargetList(character);
            }
        }
    }

}
