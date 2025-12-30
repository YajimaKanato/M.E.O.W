using UnityEngine;

namespace DataDriven
{
    /// <summary>イベントのベースデータ</summary>
    [CreateAssetMenu(fileName = "EventData", menuName = "Event/EventData")]
    public class EventData : ScriptableObject
    {
        [SerializeField] EventTextData[] _events;

        public EventTextData[] Events => _events;
    }

    /// <summary>イベントテキストに関するクラス</summary>
    [System.Serializable]
    public class EventTextData
    {
        [SerializeField, TextArea] string[] _eventTexts;

        public string[] EventTexts => _eventTexts;
    }
}
