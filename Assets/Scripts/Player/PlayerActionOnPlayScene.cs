using UnityEngine;
using UnityEngine.InputSystem;
using Interface;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerActionOnPlayScene : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] LayerMask _groundLayer;
    Rigidbody2D _rb2d;
    GameObject _target;
    PlayerInputActionManager _playerInputActionManager;
    GameActionManager _gameActionManager;

    IItemBaseEffective _item;

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
        _playerInputActionManager.RegisterAct(_playerInputActionManager.DownAct, Down);
        _playerInputActionManager.RegisterAct(_playerInputActionManager.JumpAct, Jump);
        _playerInputActionManager.RegisterAct(_playerInputActionManager.InteractAct, EventAction);
        _playerInputActionManager.RegisterAct(_playerInputActionManager.ItemAct, ItemUse);
        _playerInputActionManager.RegisterAct(_playerInputActionManager.EnterAct, PushEnter);
    }

    private void OnDisable()
    {
        _playerInputActionManager.UnregisterAct(_playerInputActionManager.DownAct, Down);
        _playerInputActionManager.UnregisterAct(_playerInputActionManager.JumpAct, Jump);
        _playerInputActionManager.UnregisterAct(_playerInputActionManager.InteractAct, EventAction);
        _playerInputActionManager.UnregisterAct(_playerInputActionManager.ItemAct, ItemUse);
        _playerInputActionManager.UnregisterAct(_playerInputActionManager.EnterAct, PushEnter);
    }

    // Update is called once per frame
    void Update()
    {
        //移動に関する処理
        _move = _playerInputActionManager.MoveAct.ReadValue<Vector2>() * _playerInfo.Status.Speed;

        //接地判定を取る処理
        _rayStart = transform.position + new Vector3(-0.5f, -0.6f);
        _rayEnd = transform.position + new Vector3(0.5f, -0.6f);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);

        //インタラクト対象を取得する処理
        _target = _gameActionManager.GetTarget(transform);
    }

    private void FixedUpdate()
    {
        Move(_playerInputActionManager.RunAct.IsPressed());
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerInputActionManager = PlayerInputActionManager.Instance;
        _gameActionManager = GameActionManager.Instance;
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
            _gameActionManager.ChangeActionMap();
            _gameActionManager.Interact(_target.GetComponent<EventBase>(), _playerInfo);
        }
        else
        {
            Debug.Log("Not Event");
        }
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void ItemSelectForKeyboard(InputAction.CallbackContext context)
    {
        //_gameActionManager.ItemSelectForKeyboard();
    }

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    /// <param name="context"></param>
    void ItemSelectForGamepad(InputAction.CallbackContext context)
    {
        //_gameActionManager.ItemSelectForGamepad();
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    void ItemUse(InputAction.CallbackContext context)
    {
        if (_item != null)
        {
            _gameActionManager.ItemUse(_item, _playerInfo);
        }
    }

    /// <summary>
    /// エンターを押したときに行う関数
    /// </summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        _gameActionManager.PushEnterUntilTalking();
    }
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            _gameActionManager.AddTargetList(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            _gameActionManager.RemoveTargetList(collision.gameObject);
        }
    }
}
