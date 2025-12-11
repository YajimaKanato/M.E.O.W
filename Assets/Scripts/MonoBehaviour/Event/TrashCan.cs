using System.Collections;
using UnityEngine;

public class TrashCan : CharacterNPC
{
    [SerializeField] TrashCanEventData _trashCan;
    public override bool Init(GameManager manager)
    {
        if (!base.Init(manager)) InitializeManager.FailedInitialization();
        _runtimeDataManager.RegisterData(_id, new TrashCanEventRunTime(_trashCan));
        return _isInitialized;

    }

    public override IEnumerator Event()
    {
        return _runtimeDataManager.GetData<TrashCanEventRunTime>(_id).Event();
    }
}
