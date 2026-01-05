using DataDriven.Item;
using UnityEngine;

namespace DataDriven
{
    /// <summary>条件によって進行が変わるイベントのクラス</summary>
    [CreateAssetMenu(fileName = "ConditionalNextEvent", menuName = "Event/EventParts/ConditionalNextEvent")]
    public class ConditionalNextEvent : EventParts
    {
        [SerializeField] NextEvent[] _nextEvent;
        [SerializeField] EventData _failedEvent;

        public NextEvent[] nextEvent => _nextEvent;
        public EventData FailedEvent => _failedEvent;
    }

    /// <summary>次のイベントに進む条件と次のイベントを管理するクラス</summary>
    [System.Serializable]
    public class NextEvent
    {
        [SerializeField] ItemName _conditionalItem;
        [SerializeField] EventData _event;

        public ItemName ConditionalItem => _conditionalItem;
        public EventData Event => _event;
    }
}
