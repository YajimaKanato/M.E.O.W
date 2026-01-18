using System;
using UnityEngine;

namespace DataDriven
{
    public class PlayerActionConnector
    {
        public event Action<Vector2, float, float> MoveAct;
        public event Action<bool> DownAct;
        public event Action<bool> RunAct;
        public event Action<Vector2> JumpAct;

        /// <summary>
        /// 移動時の処理を行う関数
        /// </summary>
        /// <param name="move">移動する方向</param>
        /// <param name="maxSpeed">最大速度</param>
        /// <param name="accel">減速率</param>
        public void Move(Vector2 move, float maxSpeed, float accel)
        {
            MoveAct?.Invoke(move, maxSpeed, accel);
        }

        /// <summary>
        /// 足場から降りる時の処理を行う関数
        /// </summary>
        /// <param name="down">足場から降りたかどうか</param>
        public void Down(bool down)
        {
            DownAct?.Invoke(down);
        }

        /// <summary>
        /// 走るときの処理を行う関数
        /// </summary>
        /// <param name="run">走るかどうか</param>
        public void Run(bool run)
        {
            RunAct?.Invoke(run);
        }

        /// <summary>
        /// ジャンプするときの処理を行う関数
        /// </summary>
        /// <param name="jumpPower">ジャンプ力</param>
        public void Jump(Vector2 jumpPower)
        {
            JumpAct?.Invoke(jumpPower);
        }
    }
}
