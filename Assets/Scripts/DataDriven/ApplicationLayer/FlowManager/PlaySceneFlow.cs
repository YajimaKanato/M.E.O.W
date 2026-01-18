using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    public class PlaySceneFlow : FlowBase
    {
        PlaySceneSystem _playSceneSystem;

        public override void Init(GameFlowManager gameFlowManager, RuntimeDataRepository repository, UnityConnector connector)
        {
            _gameFlowManager = gameFlowManager;
            _playSceneSystem = new PlaySceneSystem(repository, connector.ActionConnector);
        }

        #region PlayScene
        /// <summary>
        /// 移動時の処理を行う関数
        /// </summary>
        /// <param name="move">移動する方向</param>
        /// <param name="position">現在位置</param>
        public void Move(Vector2 move, Vector3 position)
        {
            _playSceneSystem.Move(move);
            if (move != Vector2.zero)
            {
                GetTarget(position);
            }
        }

        /// <summary>
        /// 足場から降りる時の処理を行う関数
        /// </summary>
        /// <param name="down">足場から降りたかどうか</param>
        public void Down(bool down)
        {
            _playSceneSystem.Down(down);
        }

        /// <summary>
        /// 走るときの処理を行う関数
        /// </summary>
        /// <param name="run">走るかどうか</param>
        public void Run(bool run)
        {
            _playSceneSystem.Run(run);
        }

        /// <summary>
        /// ジャンプするときの処理を行う関数
        /// </summary>
        public void Jump()
        {
            _playSceneSystem.Jump();
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectForKeyboard(int index)
        {
            _playSceneSystem.HotbarSelectForKetboard(index);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarselectForGamePad(IndexMove dir)
        {
            _playSceneSystem.HotbarSelectForGamePad(dir);
        }

        public void UseItem()
        {
            _playSceneSystem.UseItem();
        }
        #endregion

        /// <summary>
        /// ターゲットのリストに登録する関数
        /// </summary>
        /// <param name="target">登録するターゲット</param>
        public void AddTargetList(InteractMono target)
        {
            _playSceneSystem.AddTargetList(target);
        }

        /// <summary>
        /// ターゲットのリストから削除する関数
        /// </summary>
        /// <param name="target">削除するターゲット</param>
        public void RemoveTargetList(InteractMono target)
        {
            _playSceneSystem.RemoveTargetList(target);
        }

        /// <summary>
        /// 一番近いターゲットを返す関数
        /// </summary>
        /// <param name="position">現在位置</param>
        public DataID GetTarget(Vector3 position)
        {
            return _playSceneSystem.GetTarget(position);
        }
    }
}
