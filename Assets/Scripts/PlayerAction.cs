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

    InputAction _moveAct, _jumpAct, _runAct;
    Rigidbody2D _rb2d;

    Vector3 _move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAct = InputSystem.actions.FindAction("Move");
        _jumpAct = InputSystem.actions.FindAction("Jump");
        //Jump‚ÉŠ„‚è“–‚Ä‚ç‚ê‚½‚à‚Ì‚ð‰Ÿ‚µ‚½uŠÔ‚ÉŒÄ‚Î‚ê‚é
        _jumpAct.started += a => _rb2d.AddForce(Vector3.up * _jump, ForceMode2D.Impulse);
        _runAct = InputSystem.actions.FindAction("Run");
        _rb2d = GetComponent<Rigidbody2D>();
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
}
