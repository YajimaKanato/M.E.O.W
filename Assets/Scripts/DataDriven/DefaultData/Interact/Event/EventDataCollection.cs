using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>イベントのデータ</summary>
    [CreateAssetMenu(fileName = "EventDataCollection", menuName = "Event/EventDataCollection")]
    public class EventDataCollection : ScriptableObject
    {
        [SerializeField] EventData[] _events;

        /// <summary>すべてのイベントをキューにしたものを取得するプロパティ</summary>
        public Queue<EventData> Events => new Queue<EventData>(_events);
    }
}
