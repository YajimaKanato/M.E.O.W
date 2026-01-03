using UnityEngine;

namespace DataDriven
{
    /// <summary>テキストのみのイベントのクラス</summary>
    [CreateAssetMenu(fileName = "TalkEvent", menuName = "Event/EventParts/TalkEvent")]
    public class TalkEvent : EventParts
    {
        [SerializeField] string _talkerName;
        [SerializeField, TextArea] string _text;

        public string Text => _text;
        public string TalkerName => _talkerName;
    }
}
