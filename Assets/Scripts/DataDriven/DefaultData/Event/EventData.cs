using UnityEngine;
using System.Collections.Generic;

namespace DataDriven
{
    /// <summary>一つのイベントのまとまりのクラス</summary>
    [CreateAssetMenu(fileName = "EventData", menuName = "Event/EventData")]
    public class EventData : ScriptableObject
    {
        [SerializeField] EventParts[] _events;

        /// <summary>イベントの一連の流れのキューを取得するプロパティ</summary>
        public Queue<EventParts> Events => new Queue<EventParts>(_events);
    }

    /// <summary>イベントのパーツのベースクラス</summary>
    public abstract class EventParts : ScriptableObject
    {
        [SerializeField] protected EventType _eventType = EventType.Talk;

        public EventType EventType => _eventType;
    }
}
