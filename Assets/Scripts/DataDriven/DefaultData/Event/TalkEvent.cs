using UnityEngine;

namespace DataDriven
{
    /// <summary>テキストのみのイベントのクラス</summary>
    [CreateAssetMenu(fileName = "TalkEvent", menuName = "Event/EventParts/TalkEvent")]
    public class TalkEvent : EventParts
    {
        [SerializeField] TalkerName _talkerName;
        [SerializeField, TextArea] string _text;

        public TalkerName TalkerName => _talkerName;
        public string Text => _text;
    }
}
