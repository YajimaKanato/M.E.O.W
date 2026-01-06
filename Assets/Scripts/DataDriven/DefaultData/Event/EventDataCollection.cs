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
        [InspectorName("特定のアイテムが条件")] SpecificItem,
        [InspectorName("任意のアイテムが条件")] AnyItem,
        [InspectorName("次に進む")] Next,
        [InspectorName("繰り返す")] Loop
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
