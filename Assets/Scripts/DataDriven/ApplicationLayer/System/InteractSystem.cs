using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>インタラクトの処理を司るクラス</summary>
    public class InteractSystem
    {
        Queue<EventParts> _event;

        /// <summary>
        /// インタラクトを開始する時に呼び出す関数
        /// </summary>
        /// <param name="character">開始するインタラクトの対象キャラクター</param>
        public void StartInteract(CharacterRuntime character)
        {
            _event = character.EventExecute().Events;
            PushInteract();
        }

        /// <summary>
        /// インタラクトを進める関数
        /// </summary>
        public void PushInteract()
        {
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
                    break;
                default:
                    break;
            }
        }
    }
}
