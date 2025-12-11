using System.Collections;
using UnityEngine;

public class Dog : CharacterNPC
{
    [SerializeField] DogData _data;
    [SerializeField] DogEventData _eventData;
    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new DogRnutimeData(_data));
        _runtimeDataManager.RegisterData(_id, new DogEventRunTime(_eventData));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _runtimeDataManager.GetData<DogEventRunTime>(_id).Event();
    }
}
