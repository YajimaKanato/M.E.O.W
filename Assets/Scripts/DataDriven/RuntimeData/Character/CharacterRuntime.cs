using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>キャラクターのランタイムベースデータ</summary>
    public class CharacterRuntime : IRuntime
    {
        protected Queue<EventData> _event;

        /// <summary>
        /// 現在実行するイベントテキストを返す関数
        /// </summary>
        /// <returns>現在実行するイベントテキスト</returns>
        public EventData EventExecute()
        {
            return _event.Peek();
        }

        /// <summary>
        /// 実行するイベントを進める関数
        /// </summary>
        public void NextEvent()
        {
            _event.Dequeue();
        }
    }

    /// <summary>犬のランタイムデータ</summary>
    public class DogRuntime : CharacterRuntime
    {
        public DogRuntime(DogDefaultData dog)
        {
            _event = dog.EventData.Events;
        }
    }
}
