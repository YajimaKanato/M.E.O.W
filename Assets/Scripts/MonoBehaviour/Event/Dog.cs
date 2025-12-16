using System.Collections;
using UnityEngine;

/// <summary>犬に関する制御を行うクラス</summary>
public class Dog : CharacterNPC
{
    [SerializeField] DogData _data;
    [SerializeField] DogEventData _eventData;
    DogEventRunTime _dogEventRuntime;

    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new DogRnutimeData(_data));
        _runtimeDataManager.RegisterData(_id, new DogEventRunTime(_eventData));
        _isInitialized = InitializeManager.InitializationForVariable(out _dogEventRuntime, _runtimeDataManager.GetData<DogEventRunTime>(_id));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _dogEventRuntime.Event();
    }
}
