using UnityEngine;

/// <summary>初期化処理をまとめて行うためのベースクラス</summary>
public class InitializeBehaviour : MonoBehaviour
{
    [SerializeField,Tooltip("ID")] protected int _id;
    protected GameManager _gameManager;
    protected RuntimeDataManager _runtimeDataManager;
    protected ObjectManager _objectManager;
    protected bool _isInitialized = true;
    public int ID => _id;

    public virtual bool Init(GameManager manager)
    {
        Debug.LogError("Please Override Init()!");
        return false;
    }
}
