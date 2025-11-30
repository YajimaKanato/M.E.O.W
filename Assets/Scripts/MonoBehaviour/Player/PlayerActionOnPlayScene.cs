using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerActionOnPlayScene : InitializeBehaviour
{
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckDistance = -0.6f;
    Rigidbody2D _rb2d;
    Animator _animator;

    RaycastHit2D _groundHit;
    Vector3 _move;
    Vector3 _rayStart, _rayEnd;

    #region Unityメッセージなど
    public override bool Init(GameManager manager)
    {

        if (TryGetComponent<Rigidbody2D>(out var rb2d))
        {
            _rb2d = rb2d;
        }
        else
        {
            FailedInitialization();
        }

        if (TryGetComponent<Animator>(out var animator))
        {
            _animator = animator;
        }
        else
        {
            FailedInitialization();
        }

        _gameManager = manager;
        if (!_gameManager)
        {
            FailedInitialization();
        }
        else
        {
            if (_gameManager.PlayerInputActionManager == null)
            {
                FailedInitialization();
            }
            else if (_gameManager.DataManager == null)
            {
                FailedInitialization();
            }
            else if (_gameManager.GameActionManager == null)
            {
                FailedInitialization();
            }
            else
            {
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.DownActOnPlayScene, Down);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.JumpActOnPlayScene, Jump);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.InteractActOnPlayScene, EventAction);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.ItemActOnPlayScene, ItemUse);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.ItemSlotActOnPlayScene, ItemSelectForKeyboard);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SlotNextActOnPlayScene, SlotNextForGamepad);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.SlotBackActOnPlayScene, SlotBackForGamepad);
                _gameManager.PlayerInputActionManager.RegisterAct(_gameManager.PlayerInputActionManager.MenuActOnPlayScene, OpenMenu);
            }
        }
        return _isInitialized;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isInitialized) return;
        //移動に関する処理
        _move = _gameManager.PlayerInputActionManager.MoveActOnPlayScene.ReadValue<Vector2>() * _gameManager.DataManager.PlayerRunTimeOnPlayScene.Speed;
        transform.localScale = new Vector3(_move.x > 0 ? -1 : _move.x < 0 ? 1 : transform.localScale.x, 1, 1);

        //接地判定を取る処理
        _rayStart = transform.position + new Vector3(-0.5f, _groundCheckDistance);
        _rayEnd = transform.position + new Vector3(0.5f, _groundCheckDistance);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);

        //インタラクト対象を取得する処理
        _gameManager.GameActionManager.GetTarget(transform);
    }

    private void FixedUpdate()
    {
        if (!_isInitialized) return;
        //ダッシュか否か
        if (_gameManager.PlayerInputActionManager.RunActOnPlayScene.IsPressed())
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _gameManager.DataManager.PlayerRunTimeOnPlayScene.MaxRunSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
        else
        {
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _gameManager.DataManager.PlayerRunTimeOnPlayScene.MaxWalkSpeed)
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
        if (_groundHit) _rb2d.AddForce(Vector3.up * _gameManager.DataManager.PlayerRunTimeOnPlayScene.Jump, ForceMode2D.Impulse);
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
    /// メニューを開く関数
    /// </summary>
    /// <param name="context"></param>
    void OpenMenu(InputAction.CallbackContext context)
    {
        _gameManager.GameActionManager.OpenMenu();
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
