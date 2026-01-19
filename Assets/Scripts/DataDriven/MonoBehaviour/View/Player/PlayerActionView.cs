using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーのアクションを管理するクラス</summary>
    public class PlayerActionView : ViewBase
    {
        [SerializeField, Tooltip("地面のレイヤー")] LayerMask _groundLayer;
        [SerializeField, Tooltip("接地判定をする距離")] float _groundCheckDistance = -0.6f;
        Rigidbody2D _rb2d;
        Animator _animator;
        PlayerActionConnector _connector;
        RaycastHit2D _groundHit;
        Vector2 _move;
        Vector3 _rayStart, _rayEnd;
        bool _isInitialized;
        float _maxSpeed;
        float _acceleration;

        public override void Init(UnityConnector connector)
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _connector = connector.ActionConnector;
            _isInitialized = true;
            FuncRegister();
        }

        protected override void FuncRegister()
        {
            if (_connector == null) return;
            _connector.MoveAct += Move;
            _connector.DownAct += Down;
            _connector.RunAct += Run;
            _connector.JumpAct += Jump;
        }

        protected override void FuncRemove()
        {
            if (_connector == null) return;
            _connector.MoveAct -= Move;
            _connector.DownAct -= Down;
            _connector.RunAct -= Run;
            _connector.JumpAct -= Jump;
        }

        private void Update()
        {
            if (!_isInitialized) return;
            //速度制限
            if (Mathf.Abs(_rb2d.linearVelocityX) < _maxSpeed)
            {
                _rb2d.linearVelocityX += _move.x;
            }
            _rb2d.linearVelocityX *= _acceleration;
        }

        /// <summary>
        /// 移動時の処理を行う関数
        /// </summary>
        /// <param name="move">移動する方向</param>
        /// <param name="maxSpeed">最大速度</param>
        /// <param name="accel">減速率</param>
        void Move(Vector2 move, float maxSpeed, float accel)
        {
            _move = move;
            _maxSpeed = maxSpeed;
            _acceleration = accel;
        }

        /// <summary>
        /// 足場から降りる時の処理を行う関数
        /// </summary>
        /// <param name="down">足場から降りたかどうか</param>
        void Down(bool down)
        {

        }

        /// <summary>
        /// 走るときの処理を行う関数
        /// </summary>
        ///<param name="run">走るかどうか</param>
        void Run(bool run)
        {

        }

        /// <summary>
        /// ジャンプするときの処理を行う関数
        /// </summary>
        /// <param name="jump">ジャンプ力</param>
        public void Jump(Vector2 jump)
        {
            //接地判定を取る処理
            _rayStart = transform.position + new Vector3(-0.5f, _groundCheckDistance);
            _rayEnd = transform.position + new Vector3(0.5f, _groundCheckDistance);
            Debug.DrawLine(_rayStart, _rayEnd);
            _groundHit = Physics2D.Linecast(_rayStart, _rayEnd, _groundLayer);

            if (_groundHit) _rb2d.AddForce(jump, ForceMode2D.Impulse);
        }
    }
}
