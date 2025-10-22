using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAction : MonoBehaviour
{
    [SerializeField] float _speed = 20;
    [SerializeField] float _maxWalkSpeed = 5;
    [SerializeField] float _maxRunSpeed = 10;
    [SerializeField] float _jump = 5;
    [SerializeField] LayerMask _groundLayer;

    InputAction _moveAct, _jumpAct, _runAct, _interactAct, _itemAct;
    Rigidbody2D _rb2d;
    GameObject _target;

    RaycastHit2D _groundHit;
    Vector3 _move;
    Vector3 _rayStart, _rayEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //InputActionに割り当て
        _moveAct = InputSystem.actions.FindAction("Move");
        _jumpAct = InputSystem.actions.FindAction("Jump");
        _runAct = InputSystem.actions.FindAction("Run");
        _interactAct = InputSystem.actions.FindAction("Interact");
        _itemAct = InputSystem.actions.FindAction("Item");
        _rb2d = GetComponent<Rigidbody2D>();
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

    // Update is called once per frame
    void Update()
    {
        _move = _moveAct.ReadValue<Vector2>() * _speed;

        _rayStart = transform.position + new Vector3(-0.5f, -0.6f);
        _rayEnd = transform.position + new Vector3(0.5f, -0.6f);
        Debug.DrawLine(_rayStart, _rayEnd);
        _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);
    }

    private void FixedUpdate()
    {
        if (_runAct.IsPressed())
        {
            if (Mathf.Abs(_rb2d.linearVelocityX) < _maxRunSpeed)
            {
                _rb2d.AddForce(_move);
            }
        }
        else
        {
            if (Mathf.Abs(_rb2d.linearVelocityX) < _maxWalkSpeed)
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
        if (_groundHit) _rb2d.AddForce(Vector3.up * _jump, ForceMode2D.Impulse);
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

    void OpenItemList(InputAction.CallbackContext context)
    {
        ButtonActions.ChangeScene("Bag");
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
        else
        {
            if (collision.tag == "Event")
            {
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
