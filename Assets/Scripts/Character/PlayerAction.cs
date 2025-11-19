using UnityEngine;
using UnityEngine.InputSystem;
using Interface;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] PlayerInputActionManager _playerInputActions;
    [SerializeField] LayerMask _groundLayer;
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
        _playerInputActions.RegisterAct(_playerInputActions.DownAct, Down);
        _playerInputActions.RegisterAct(_playerInputActions.JumpAct, Jump);
        _playerInputActions.RegisterAct(_playerInputActions.InteractAct, EventAction);
        _playerInputActions.RegisterAct(_playerInputActions.ItemAct, ItemUse);
        _playerInputActions.RegisterAct(_playerInputActions.EnterAct, PushEnter);
    }

    private void OnDisable()
    {
        _playerInputActions.UnregisterAct(_playerInputActions.DownAct, Down);
        _playerInputActions.UnregisterAct(_playerInputActions.JumpAct, Jump);
        _playerInputActions.UnregisterAct(_playerInputActions.InteractAct, EventAction);
        _playerInputActions.UnregisterAct(_playerInputActions.ItemAct, ItemUse);
        _playerInputActions.UnregisterAct(_playerInputActions.EnterAct, PushEnter);
    }

    // Update is called once per frame
    void Update()
    {
        //移動に関する処理
        _move = _playerInputActions.MoveAct.ReadValue<Vector2>() * _playerInfo.Status.Speed;

        //接地判定を取る処理
        _rayStart = transform.position + new Vector3(-0.5f, -0.6f);
        _rayEnd = transform.position + new Vector3(0.5f, -0.6f);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);

        //インタラクト対象を取得する処理
        _target = GameActionManager.Instance.GetTarget(transform);
    }

    private void FixedUpdate()
    {
        //Move(_runAct.IsPressed());
        Move(_playerInputActions.RunAct.IsPressed());
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
            GameActionManager.Instance.ChangeActionMap();
            GameActionManager.Instance.Interact(_target.GetComponent<EventBase>(), _playerInfo);
        }
        else
        {
            Debug.Log("Not Event");
        }
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    void ItemUse(InputAction.CallbackContext context)
    {
        if (_item != null)
        {
            GameActionManager.Instance.ItemUse(_item, _playerInfo);
        }
    }

    /// <summary>エンターを押したときに行う関数</summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        GameActionManager.Instance.PushEnterUntilTalking();
    }
    #endregion

    /// <summary>
    /// 使用するアイテムを選ぶ関数
    /// </summary>
    void ItemSelect()
    {
        _item = _playerInfo.ItemSlot.SelectItem(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            GameActionManager.Instance.AddTargetList(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Event"))
        {
            GameActionManager.Instance.RemoveTargetList(collision.gameObject);
        }
    }
}
