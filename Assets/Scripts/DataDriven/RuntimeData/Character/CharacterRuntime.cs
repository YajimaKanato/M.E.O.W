using UnityEngine;

namespace DataDriven
{
    /// <summary>キャラクターのランタイムベースデータ</summary>
    public class CharacterRuntime
    {
        protected EventData _event;
        protected int _currentEvent = 0;

        /// <summary>
        /// 現在実行するイベントテキストを返す関数
        /// </summary>
        /// <returns>現在実行するイベントテキスト</returns>
        public EventTextData EventExecute()
        {
            return _event.Events[_currentEvent];
        }

        /// <summary>
        /// 実行するイベントを進める関数
        /// </summary>
        public void NextEvent()
        {
            _currentEvent++;
            if (_currentEvent > _event.Events.Length - 1) _currentEvent--;
        }
    }

    /// <summary>犬のランタイムデータ</summary>
    public class DogRuntime : CharacterRuntime
    {
        public DogRuntime(DogDefaultData dog)
        {
            _event = dog.EventData;
        }
    }
}
