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

        public InteractSystem(RuntimeDataRepository repository)
        {
            _repository = repository;
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
                    var talk = (TalkEvent)parts;
                    Debug.Log($"{talk.TalkerName} : {talk.Text}");
                    break;
                case EventType.GiveItem:
                    var give = (GiveItemEvent)parts;
                    if (_repository.TryGetData<PlayerRuntimeData>((int)EntityID.Player, out var data))
                    {
                        data.GetItem(give.Item);
                    }
                    foreach (var item in data.Hotbar.Hotbar)
                    {
                        Debug.Log(item ? item.ItemName : "null");
                    }
                    break;
                case EventType.Next:
                    _target.NextEvent();
                    _target = null;
                    _event = null;
                    Debug.Log("NextEvent");
                    break;
                case EventType.ConditionalNext:
                    break;
                default:
                    break;
            }
        }
    }
}
