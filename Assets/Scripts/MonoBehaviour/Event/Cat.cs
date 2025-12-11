using System.Collections;
using UnityEngine;

public class Cat : CharacterNPC
{
    [SerializeField] CatData _data;
    [SerializeField] CatEventData _eventData;
    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new CatRuntimeData(_data));
        _runtimeDataManager.RegisterData(_id, new CatEventRunTime(_eventData));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _runtimeDataManager.GetData<CatEventRunTime>(_id).Event();
    }
}
