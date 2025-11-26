using UnityEngine;

[CreateAssetMenu(fileName = "EventDataList", menuName = "Event/EventDataList")]
public class EventDataList : ScriptableObject
{
    [SerializeField] EventBaseData[] _eventList;
    public EventBaseData[] EventList => _eventList;
}
