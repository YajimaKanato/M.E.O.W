using System.Collections;
using UnityEngine;

/// <summary>ネズミに関する制御を行うクラス</summary>
public class Mouse : CharacterNPC
{
    [SerializeField, Tooltip("ネズミの初期データ")] MouseData _data;
    [SerializeField, Tooltip("ネズミのイベントデータ")] MouseEventData _eventData;
    MouseEventRunTime _mouseEventRuntime;

    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new MouseRuntimeData(_data));
        _runtimeDataManager.RegisterData(_id, new MouseEventRunTime(_eventData));
        _isInitialized = InitializeManager.InitializationForVariable(out _mouseEventRuntime, _runtimeDataManager.GetData<MouseEventRunTime>(_id));
        return _isInitialized;
    }

    public override IEnumerator Event()
    {
        return _mouseEventRuntime.Event();
    }
}
