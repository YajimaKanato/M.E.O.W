using System;
using UnityEngine;

namespace DataDriven
{
    /// <summary>Unityとデータをつなげる役割のクラス</summary>
    public class UnityConnector
    {
        public event Action<Vector2, float> _moveAct;
        public event Action<bool> _downAct;
        public event Action<bool> _runAct;
        public event Action<bool, float> _jumpAct;

        /// <summary>
        /// 移動時の処理を行う関数
        /// </summary>
        /// <param name="move">移動する方向</param>
        /// <param name="maxSpeed">最大速度</param>
        public void Move(Vector2 move, float maxSpeed)
        {
            _moveAct?.Invoke(move, maxSpeed);
        }

        /// <summary>
        /// 足場から降りる時の処理を行う関数
        /// </summary>
        /// <param name="down">足場から降りたかどうか</param>
        public void Down(bool down)
        {
            _downAct?.Invoke(down);
        }

        /// <summary>
        /// 走るときの処理を行う関数
        /// </summary>
        /// <param name="run">走るかどうか</param>
        public void Run(bool run)
        {
            _runAct?.Invoke(run);
        }

        /// <summary>
        /// ジャンプするときの処理を行う関数
        /// </summary>
        /// <param name="jump">ジャンプするかどうか</param>
        /// <param name="jumpPower">ジャンプ力</param>
        public void Jump(bool jump, float jumpPower)
        {
            _jumpAct?.Invoke(jump, jumpPower);
        }
    }
}
