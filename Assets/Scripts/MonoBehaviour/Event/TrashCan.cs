using System.Collections;
using UnityEngine;

/// <summary>ゴミ箱に関する制御を行うクラス</summary>
public class TrashCan : CharacterNPC
{
    [SerializeField] TrashCanEventData _trashCan;
    TrashCanEventRunTime _trashCanEventRuntime;
    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new TrashCanEventRunTime(_trashCan));
        _isInitialized = InitializeManager.InitializationForVariable(out _trashCanEventRuntime, _runtimeDataManager.GetData<TrashCanEventRunTime>(_id));
        return _isInitialized;

    }

    public override IEnumerator Event()
    {
        return _trashCanEventRuntime.Event();
    }
}
