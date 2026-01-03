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

    /// <summary>イベントの種類のラベル</summary>
    public enum EventType
    {
        [InspectorName("会話")] Talk,
        [InspectorName("アイテムをあげる")] GiveItem,
    }

    /// <summary>話し手の名前</summary>
    public enum TalkerName
    {
        [InspectorName("主人公")] Player,
        [InspectorName("犬")] Dog,
        [InspectorName("野良猫")] Cat,
        [InspectorName("ネズミ")] Mouse,
        [InspectorName("アンドロイド")] Android,
        [InspectorName("なし")] Unknown
    }
}
