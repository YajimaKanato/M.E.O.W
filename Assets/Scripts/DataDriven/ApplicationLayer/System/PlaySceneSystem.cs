using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイ中の処理を司るクラス</summary>
    public class PlaySceneSystem
    {
        RuntimeDataRepository _repository;
        PlayerActionConnector _connector;
        List<InteractMono> _targetList;
        InteractMono _target;
        bool _isRun;

        public PlaySceneSystem(RuntimeDataRepository repository, PlayerActionConnector connector)
        {
            _repository = repository;
            _connector = connector;
            _targetList = new List<InteractMono>();
        }

        /// <summary>
        /// 移動時の処理を行う関数
        /// </summary>
        /// <param name="move">移動する方向</param>
        public void Move(Vector2 move)
        {
            if (_repository.TryGetData<PlayerRuntimeData>(DataID.Player, out var player))
            {
                if (_isRun)
                {
                    _connector.Move(move * player.RunSpeed, player.MaxRunSpeed, player.Acceleration);
                }
                else
                {
                    _connector.Move(move * player.WalkSpeed, player.MaxWalkSpeed, player.Acceleration);
                }
            }
        }

        /// <summary>
        /// 足場から降りる時の処理を行う関数
        /// </summary>
        /// <param name="down">足場から降りたかどうか</param>
        public void Down(bool down)
        {
            _connector.Down(down);
        }

        /// <summary>
        /// 走るときの処理を行う関数
        /// </summary>
        /// <param name="run">走るかどうか</param>
        public void Run(bool run)
        {
            _isRun = run;
            if (_repository.TryGetData<PlayerRuntimeData>(DataID.Player, out var player))
            {
                _connector.Run(_isRun);
            }
        }

        /// <summary>
        /// ジャンプするときの処理を行う関数
        /// </summary>
        public void Jump()
        {
            if (_repository.TryGetData<PlayerRuntimeData>(DataID.Player, out var player))
            {
                _connector.Jump(Vector3.up * player.Jump);
            }
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectForKetboard(int index)
        {
            if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
            {
                hotbar.SelectItemForKeyboard(index);
            }
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarSelectForGamePad(IndexMove dir)
        {
            if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
            {
                hotbar.SelectItemForGamePad(dir);
            }
        }

        /// <summary>
        /// アイテムを使用する関数
        /// </summary>
        public void UseItem()
        {
            if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
            {
                var item = hotbar.UseItem();
                //空のスロットを選択した時
                if (!item) return;
                if (_repository.TryGetData<PlayerRuntimeData>(DataID.Player, out var player))
                {
                    //悪影響を及ぼす食べ物の場合ダメージ
                    if (item.ItemType == ItemRole.BadFood) player.ChangeHP(((BadFoodDefaultData)item).Damage * (-1));
                    player.Saturation(item.Saturate);
                    Debug.Log($"HP => {player.CurrentHP}\nSaturation => {player.CurrentFullness}");
                }
            }
        }

        /// <summary>
        /// ターゲットのリストに登録する関数
        /// </summary>
        /// <param name="target">登録するターゲット</param>
        public void AddTargetList(InteractMono target)
        {
            _targetList.Add(target);
        }

        /// <summary>
        /// ターゲットのリストから削除する関数
        /// </summary>
        /// <param name="target">削除するターゲット</param>
        public void RemoveTargetList(InteractMono target)
        {
            _targetList.Remove(target);
        }

        /// <summary>
        /// 一番近いターゲットを返す関数
        /// </summary>
        /// <param name="position">現在位置</param>
        public DataID GetTarget(Vector3 position)
        {
            _target = null;
            foreach (InteractMono target in _targetList)
            {
                if (_target)
                {
                    var dir = Vector3.SqrMagnitude(_target.transform.position - position);
                    var compareDir = Vector3.SqrMagnitude(target.transform.position - position);
                    if (dir > compareDir) _target = target;
                }
                else
                {
                    _target = target;
                }
            }
            return _target ? _target.ID : default;
        }
    }
}
