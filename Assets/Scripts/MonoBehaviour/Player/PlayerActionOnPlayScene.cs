using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>プレイヤーの動きに関する制御を行うクラス</summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerActionOnPlayScene : InitializeBehaviour
{
    [SerializeField, Tooltip("プレイヤーの初期データ")] PlayerDataOnPlayScene _data;
    [SerializeField, Tooltip("地面のレイヤー")] LayerMask _groundLayer;
    [SerializeField, Tooltip("接地判定をする距離")] float _groundCheckDistance = -0.6f;
    Rigidbody2D _rb2d;
    Animator _animator;
    PlayerInputActionManager _playerInputActionManager;
    PlayerRunTimeOnPlayScene _playerRunTimeOnPlayScene;
    GameActionManager _gameActionManager;
    ObjectManager _dataManager;

    RaycastHit2D _groundHit;
    Vector3 _move;
    Vector3 _rayStart, _rayEnd;

    #region Unityメッセージなど
    public override bool Init(GameManager manager)
    {

        if (!TryGetComponent(out _rb2d)) _isInitialized = InitializeManager.FailedInitialization();

        if (!TryGetComponent(out _animator)) _isInitialized = InitializeManager.FailedInitialization();

        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _playerInputActionManager, _gameManager.PlayerInputActionManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _gameActionManager, _gameManager.GameActionManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _dataManager, _gameManager.ObjectManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);

        //ランタイムデータ
        _runtimeDataManager.RegisterData(_id, new PlayerRunTimeOnPlayScene(_data));
        _isInitialized = InitializeManager.InitializationForVariable(out _playerRunTimeOnPlayScene, _runtimeDataManager.GetData<PlayerRunTimeOnPlayScene>(_id));

        if (_isInitialized)
        {
            _playerInputActionManager.RegisterAct(_playerInputActionManager.DownActOnPlayScene, Down);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.JumpActOnPlayScene, Jump);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.InteractActOnPlayScene, EventAction);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.ItemActOnPlayScene, ItemUse);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.ItemSlotActOnPlayScene, ItemSelectForKeyboard);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.SlotNextActOnPlayScene, SlotNextForGamepad);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.SlotBackActOnPlayScene, SlotBackForGamepad);
            _playerInputActionManager.RegisterAct(_playerInputActionManager.MenuActOnPlayScene, OpenMenu);
        }
        return _isInitialized;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isInitialized) return;
        //移動に関する処理
        _move = _playerInputActionManager.MoveActOnPlayScene.ReadValue<Vector2>() * _playerRunTimeOnPlayScene.Speed;
        transform.localScale = new Vector3(_move.x > 0 ? -1 : _move.x < 0 ? 1 : transform.localScale.x, 1, 1);

        //接地判定を取る処理
        _rayStart = transform.position + new Vector3(-0.5f, _groundCheckDistance);
        _rayEnd = transform.position + new Vector3(0.5f, _groundCheckDistance);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);

        //インタラクト対象を取得する処理
        _dataManager.GetTarget(transform);
    }

    private void FixedUpdate()
    {
        if (!_isInitialized) return;
        //ダッシュか否か
        if (_playerInputActionManager.RunActOnPlayScene.IsPressed())
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _playerRunTimeOnPlayScene.MaxRunSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
        else
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _playerRunTimeOnPlayScene.MaxWalkSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
    }

    private void LateUpdate()
    {
        if (!_isInitialized) return;
        _animator.SetFloat("Move", Mathf.Abs(_rb2d.linearVelocityX));
        _animator.SetBool("Ground", _groundHit);
    }

    #endregion

    #region InputSystem関連
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
        if (_groundHit) _rb2d.AddForce(Vector3.up * _playerRunTimeOnPlayScene.Jump, ForceMode2D.Impulse);
    }

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <param name="context"></param>
    void EventAction(InputAction.CallbackContext context)
    {
        _gameActionManager.Interact();
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
        _gameActionManager.SelectForKeyboard(int.Parse(key) - 1);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotNextForGamepad(InputAction.CallbackContext context)
    {
        _gameActionManager.SelectForGamepad(1);
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void SlotBackForGamepad(InputAction.CallbackContext context)
    {
        _gameActionManager.SelectForGamepad(-1);
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    void ItemUse(InputAction.CallbackContext context)
    {
        _gameActionManager.ItemUse(_id);
    }

    /// <summary>
    /// メニューを開く関数
    /// </summary>
    /// <param name="context"></param>
    void OpenMenu(InputAction.CallbackContext context)
    {
        _gameActionManager.OpenMenu();
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            if (collision.gameObject.TryGetComponent<InteractBase>(out var character))
            {
                _dataManager.AddTargetList(character);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            if (collision.gameObject.TryGetComponent<InteractBase>(out var character))
            {
                _dataManager.RemoveTargetList(character);
            }
        }
    }

}
