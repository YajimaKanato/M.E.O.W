using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] float _walkSpeed = 5;
    [SerializeField] float _runSpeed = 10;
    [SerializeField] float _jump = 5;
    [SerializeField] float _gravity = 9.8f;
    [SerializeField] float _gravityScale = 5;

    InputAction _moveAct, _jumpAct, _runAct, _interactAct, _itemAct;
    Rigidbody2D _rb2d;
    GameObject _target;

    Vector3 _move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _moveAct = InputSystem.actions.FindAction("Move");
        _jumpAct = InputSystem.actions.FindAction("Jump");
        //Jumpに割り当てられたものを押した瞬間に呼ばれる
        _jumpAct.started += a => _rb2d.AddForce(Vector3.up * _jump, ForceMode2D.Impulse);
        _runAct = InputSystem.actions.FindAction("Run");
        _interactAct = InputSystem.actions.FindAction("Interact");
        _itemAct = InputSystem.actions.FindAction("Item");
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _interactAct.started += EventAction;
        _itemAct.started += OpenItemList;
    }

    private void OnDisable()
    {
        _interactAct.started -= EventAction;
        _itemAct.started -= OpenItemList;
    }

    // Update is called once per frame
    void Update()
    {
        _move = _moveAct.ReadValue<Vector2>();
        _move *= _runAct.IsPressed() ? _runSpeed : _walkSpeed;

        _move.y = _rb2d.linearVelocityY;
        if (_move.y != 0)
        {
            _move.y -= _gravity * _gravityScale * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        _rb2d.linearVelocity = _move;
    }

    /// <summary>
    /// イベントを起こす関数
    /// </summary>
    /// <param name="contex"></param>
    void EventAction(InputAction.CallbackContext contex)
    {
        if (_target)
        {
            Debug.Log("Event");
        }
        else
        {
            Debug.Log("Not Event");
        }
    }

    void OpenItemList(InputAction.CallbackContext context)
    {
        Debug.Log("Open ItemList");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_target)
        {
            if (collision.tag == "Event")
            {
                _target = collision.gameObject;
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
