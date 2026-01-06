using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>インタラクトの処理を司るクラス</summary>
    public class InteractSystem
    {
        RuntimeDataRepository _repository;
        CharacterRuntime _target;
        Queue<EventParts> _event;
        ConditionalNextEvent _conditionalEvent;
        EnterType _enterType;

        public InteractSystem(RuntimeDataRepository repository)
        {
            _repository = repository;
            _enterType = EnterType.Interact;
        }

        /// <summary>
        /// インタラクトを開始する時に呼び出す関数
        /// </summary>
        /// <param name="character">開始するインタラクトの対象キャラクター</param>
        /// <returns>インタラクトを開始できたか</returns>
        public bool StartInteract(CharacterRuntime character)
        {
            //対象がいなかったりイベントがすでに起きていたりする場合はreturn
            if (character == null) return false;
            if (_event != null) return false;
            Debug.Log("Start Interact");
            //ターゲットを更新
            _target = character;
            //イベントを受け取る
            _event = character.EventExecute().Events;
            //イベントの実行
            PushInteract();
            return true;
        }

        /// <summary>
        /// インタラクトを進める関数
        /// </summary>
        /// <returns>最後のイベントだったかどうか</returns>
        public bool PushInteract()
        {
            if (_enterType != EnterType.Interact) GiveItem();

            //イベントを受け取っていなかったり空だったりする場合はreturn
            if (_event == null || _event.Count == 0) return false;
            //イベントを実行
            return InteractOutput(_event.Dequeue());
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectForKetboard(int index)
        {
            if (_enterType != EnterType.AnyItem) return;
            if (_repository.TryGetData<PlayerRuntimeData>((int)EntityID.Player, out var player))
            {
                player.ItemSelectOnConversationForKeyboard(index);
            }
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarSelectForGamePad(int dir)
        {
            if (_enterType != EnterType.AnyItem) return;
            if (_repository.TryGetData<PlayerRuntimeData>((int)EntityID.Player, out var player))
            {
                player.ItemSelectOnConversationForGamePad(dir);
            }
        }

        /// <summary>
        /// アイテムをあげる関数
        /// </summary>
        void GiveItem()
        {
            if (_repository.TryGetData<PlayerRuntimeData>((int)EntityID.Player, out var player))
            {
                //任意のアイテムをあげる場合
                if (_enterType == EnterType.AnyItem)
                {
                    var item = player.GiveItem();
                    //空のスロットを選択した時
                    if (!item) return;
                    Debug.Log($"Give => {item.ItemName}");
                    //初期値として条件に一致しなかった時のイベントを設定
                    _event = _conditionalEvent.FailedEvent.Events;
                    //プレイヤーがあげるアイテムが条件に一致するか調べる
                    foreach (var condition in _conditionalEvent.NextEvent)
                    {
                        if (item.ItemName == condition.ConditionalItem.ItemName)
                        {
                            //一致したらそれに応じたイベントを返す
                            _event = condition.Event.Events;
                            break;
                        }
                    }
                }
                //特定のアイテムをあげる場合
                else if (_enterType == EnterType.SpecificItem)
                {
                    var newEvent = _conditionalEvent.NextEvent[0];
                    //指定のアイテムを受け取れるかどうか
                    if (player.GiveSpecificItem(newEvent.ConditionalItem))
                    {
                        _event = newEvent.Event.Events;
                    }
                    else
                    {
                        _event = _conditionalEvent.FailedEvent.Events;
                    }
                }
                //エンター入力の状態設定
                _enterType = EnterType.Interact;
            }
        }

        /// <summary>
        /// インタラクトを出力する関数
        /// </summary>
        /// <param name="parts">出力するインタラクト</param>
        bool InteractOutput(EventParts parts)
        {
            switch (parts.EventType)
            {
                case EventType.Talk:
                    TalkEvent((TalkEvent)parts);
                    return false;
                case EventType.GiveItem:
                    GiveItemEvent((GiveItemEvent)parts);
                    return false;
                case EventType.SpecificItem or EventType.AnyItem:
                    ConditionalNextEvent((ConditionalNextEvent)parts);
                    return false;
                case EventType.Next:
                    NextEvent();
                    return true;
                case EventType.Loop:
                    LoopEvent();
                    return true;
                default:
                    return true;
            }
        }

        /// <summary>
        /// 会話イベントを行う関数
        /// </summary>
        /// <param name="talk">イベント</param>
        void TalkEvent(TalkEvent talk)
        {
            Debug.Log($"{talk.TalkerName.ToString()} : {talk.Text}");
        }

        /// <summary>
        /// アイテムをもらうイベントを行う関数
        /// </summary>
        /// <param name="give">イベント</param>
        void GiveItemEvent(GiveItemEvent give)
        {
            if (_repository.TryGetData<PlayerRuntimeData>((int)EntityID.Player, out var data))
            {
                data.GetItem(give.Item);
            }
            foreach (var item in data.Hotbar.Hotbar)
            {
                Debug.Log(item ? item.ItemName : "null");
            }
        }

        /// <summary>
        /// 条件によってイベントが分岐する時に呼ばれる関数
        /// </summary>
        /// <param name="conditional"></param>
        void ConditionalNextEvent(ConditionalNextEvent conditional)
        {
            Debug.Log("ConditionalEvent");
            _conditionalEvent = conditional;
            if (_conditionalEvent.EventType == EventType.SpecificItem)
            {
                _enterType = EnterType.SpecificItem;
            }
            else
            {
                _enterType = EnterType.AnyItem;
            }
        }

        /// <summary>
        /// イベントを次に進める関数
        /// </summary>
        void NextEvent()
        {
            Debug.Log("NextEvent");
            _target.NextEvent();
            _target = null;
            _event = null;
        }

        /// <summary>
        /// イベントをループさせる関数
        /// </summary>
        void LoopEvent()
        {
            Debug.Log("LoopEvent");
            _target = null;
            _event = null;
        }
    }

    /// <summary>エンターキーを押したときの入力タイプ</summary>
    enum EnterType
    {
        Menu,
        Interact,
        SpecificItem,
        AnyItem
    }
}
