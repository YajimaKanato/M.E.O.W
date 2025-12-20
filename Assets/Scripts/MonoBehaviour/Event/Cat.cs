using System.Collections;
using UnityEngine;

/// <summary>野良猫に関する制御を行うクラス</summary>
public class Cat : CharacterNPC
{
    [SerializeField, Tooltip("野良猫の初期データ")] CatData _data;
    [SerializeField, Tooltip("野良猫のランタイムデータ")] CatEventData _eventData;
    CatEventRunTime _catEventRuntime;
    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new CatRuntimeData(_data));
        _runtimeDataManager.RegisterData(_id, new CatEventRunTime(_eventData));
        _isInitialized = InitializeManager.InitializationForVariable(out _catEventRuntime, _runtimeDataManager.GetData<CatEventRunTime>(_id));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _catEventRuntime.Event();
    }
}
