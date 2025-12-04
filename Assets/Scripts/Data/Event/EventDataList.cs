using UnityEngine;

[CreateAssetMenu(fileName = "EventDataList", menuName = "Event/EventDataList")]
public class EventDataList : InitializeSO
{
    [SerializeField] EventBaseData[] _eventList;
    public EventBaseData[] EventList => _eventList;

    public override bool Init(GameManager manager)
    {
        foreach (var e in _eventList)
        {
            if (!e.Init(manager)) InitializeManager.FailedInitialization();
        }
        return _isInitialized;
    }
}
