using UnityEngine;

/// <summary>UIのベースクラス</summary>
public abstract class UIBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("ID")] protected int _id;
    protected GameManager _gameManager;
    protected RuntimeDataManager _runtimeDataManager;
    protected bool _isInitialized = true;
    public int ID => _id;

    public abstract bool Init(GameManager manager);
}

