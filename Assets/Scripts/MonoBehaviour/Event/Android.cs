using System.Collections;
using UnityEngine;

public class Android : CharacterNPC
{
    [SerializeField] AndroidData _data;
    [SerializeField] AndroidEventData _eventData;
    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new AndroidRuntimeData(_data));
        _runtimeDataManager.RegisterData(_id, new AndroidEventRunTime(_eventData));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _runtimeDataManager.GetData<AndroidEventRunTime>(_id).Event();
    }
}
