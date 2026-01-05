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
        public void StartInteract(CharacterRuntime character)
        {
            //対象がいなかったりイベントがすでに起きていたりする場合はreturn
            if (character == null) return;
            if (_event != null) return;
            //ターゲットを更新
            _target = character;
            //イベントを受け取る
            _event = character.EventExecute().Events;
            //イベントの実行
            PushInteract();
            Debug.Log("Start Interact");
        }

        /// <summary>
        /// インタラクトを進める関数
        /// </summary>
        public void PushInteract()
        {
            if (_enterType == EnterType.ItemSelect)
            {
                if (_repository.TryGetData<PlayerRuntimeData>((int)EntityID.Player, out var player))
                {
                    var item = player.GiveItem();
                    Debug.Log($"Give => {item.ItemName}");
                    //初期値として条件に一致しなかった時のイベントを設定
                    _event = _conditionalEvent.FailedEvent.Events;
                    //プレイヤーがあげるアイテムが条件に一致するか調べる
                    foreach (var condition in _conditionalEvent.nextEvent)
                    {
                        if (item.ItemName == condition.ConditionalItem)
                        {
                            //一致したらそれに応じたイベントを返す
                            _event = condition.Event.Events;
                            break;
                        }
                    }
                    //エンター入力の状態設定
                    _enterType = EnterType.Interact;
                }
            }
            //イベントを受け取っていなかったり空だったりする場合はreturn
            if (_event == null || _event.Count == 0) return;
            //イベントを実行
            InteractOutput(_event.Dequeue());
        }

        /// <summary>
        /// インタラクトを出力する関数
        /// </summary>
        /// <param name="parts">出力するインタラクト</param>
        void InteractOutput(EventParts parts)
        {
            switch (parts.EventType)
            {
                case EventType.Talk:
                    TalkEvent((TalkEvent)parts);
                    break;
                case EventType.GiveItem:
                    GiveItemEvent((GiveItemEvent)parts);
                    break;
                case EventType.ConditionalNext:
                    ConditionalNextEvent((ConditionalNextEvent)parts);
                    break;
                case EventType.Next:
                    NextEvent();
                    break;
                case EventType.Loop:
                    LoopEvent();
                    break;
                default:
                    break;
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
            _conditionalEvent = conditional;
            _enterType = EnterType.ItemSelect;
            Debug.Log("ConditionalEvent");
        }

        /// <summary>
        /// イベントを次に進める関数
        /// </summary>
        void NextEvent()
        {
            _target.NextEvent();
            _target = null;
            _event = null;
            Debug.Log("NextEvent");
        }

        /// <summary>
        /// イベントをループさせる関数
        /// </summary>
        void LoopEvent()
        {
            _target = null;
            _event = null;
            Debug.Log("LoopEvent");
        }
    }

    /// <summary>エンターキーを押したときの入力タイプ</summary>
    enum EnterType
    {
        Menu,
        Interact,
        ItemSelect
    }
}
