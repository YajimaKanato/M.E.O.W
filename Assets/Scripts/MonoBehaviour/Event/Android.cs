using System.Collections;
using UnityEngine;

/// <summary>アンドロイドに関する制御を行うクラス</summary>
public class Android : CharacterNPC
{
    [SerializeField, Tooltip("アンドロイドの初期データ")] AndroidData _data;
    [SerializeField, Tooltip("アンドロイドのイベントデータ")] AndroidEventData _eventData;
    AndroidEventRunTime _androidEventRuntime;

    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new AndroidRuntimeData(_data));
        _runtimeDataManager.RegisterData(_id, new AndroidEventRunTime(_eventData));
        _isInitialized = InitializeManager.InitializationForVariable(out _androidEventRuntime, _runtimeDataManager.GetData<AndroidEventRunTime>(_id));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _androidEventRuntime.Event();
    }
}
