using System.Collections;
using UnityEngine;

public class Mouse : CharacterNPC
{
    [SerializeField] MouseData _data;
    [SerializeField] MouseEventData _eventData;

    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new MouseRuntimeData(_data));
        _runtimeDataManager.RegisterData(_id, new MouseEventRunTime(_eventData));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _runtimeDataManager.GetData<MouseEventRunTime>(_id).Event();
    }
}
